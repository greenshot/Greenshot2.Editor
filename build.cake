#tool "xunit.runner.console"
#tool "OpenCover"
#tool nuget:?package=GitVersion.CommandLine&prerelease
#tool "docfx.console"
#tool "coveralls.io"
#tool "gitlink"
// Needed for Cake.Compression, as described here: https://github.com/akordowski/Cake.Compression/issues/3
#addin "SharpZipLib"
#addin "MagicChunks"
#addin "Cake.FileHelpers"
#addin "Cake.DocFx"
#addin "Cake.Coveralls"
#addin "Cake.Compression"

var target = Argument("target", "Build");
var configuration = Argument("configuration", "release");
var nugetApiKey = Argument("nugetApiKey", EnvironmentVariable("NuGetApiKey"));
var solutionFilePath = GetFiles("./**/*.sln").First();
var solutionName = solutionFilePath.GetFilenameWithoutExtension().FullPath;
Information("Solution path : " + solutionFilePath + " solution: " + solutionName);
// Used to publish coverage report
var coverallsRepoToken = Argument("coverallsRepoToken", EnvironmentVariable("COVERALLS_REPO_TOKEN"));

// Check if we are in a pull request, publishing of packages and coverage should be skipped
var isPullRequest = !string.IsNullOrEmpty(EnvironmentVariable("APPVEYOR_PULL_REQUEST_NUMBER"));

// Check if the commit is marked as release
var isRelease = (EnvironmentVariable("APPVEYOR_REPO_COMMIT_MESSAGE_EXTENDED")?? "").Contains("[release]");

GitVersion version;
	
Task("Default")
	.IsDependentOn("PublishCoverage")
    .IsDependentOn("PublishPackages");

// Publish the coveralls report to Coveralls.NET
Task("PublishCoverage")
    .IsDependentOn("Coverage")
    .WithCriteria(() => !BuildSystem.IsLocalBuild)
    .WithCriteria(() => !string.IsNullOrEmpty(coverallsRepoToken))
    .WithCriteria(() => !isPullRequest)
    .Does(()=>
{
	CoverallsIo("./artifacts/coverage.xml", new CoverallsIoSettings()
    {
        RepoToken = coverallsRepoToken
    });
});

// Publish the Artifact of the Package Task to the Nexus Pro
Task("PublishPackages")
    .IsDependentOn("Package")
    .WithCriteria(() => !BuildSystem.IsLocalBuild)
    .WithCriteria(() => !string.IsNullOrEmpty(nugetApiKey))
    .WithCriteria(() => !isPullRequest)
    .WithCriteria(() => isRelease)
    .Does(()=>
{
    var settings = new NuGetPushSettings {
        Source = "https://www.nuget.org/api/v2/package",
        ApiKey = nugetApiKey
    };

    var packages = GetFiles("./artifacts/*.nupkg").Where(p => !p.FullPath.Contains("symbols"));
    NuGetPush(packages, settings);
});

// Package the results of the build, if the tests worked, into a NuGet Package
Task("Package")
    .IsDependentOn("Build")
    .Does(()=>
{
	var gitLinkSettings = new GitLinkSettings {
		IsDebug = false
	};

	// Run GitLink before packaging the files
    foreach(var projectFilePath in GetFiles("./src/**/*.csproj").Where(p => !p.FullPath.Contains("Test") && !p.FullPath.Contains("packages") &&!p.FullPath.Contains("tools")))
    {
		var pdbFilePath = GetFiles(string.Format("./src/{0}/**/{0}.pdb",projectFilePath.GetFilenameWithoutExtension())).First();
        Information("Enhancing PDB: " + pdbFilePath.FullPath);
        GitLink(pdbFilePath.FullPath, gitLinkSettings);
    }

    var settings = new NuGetPackSettings 
    {
        OutputDirectory = "./artifacts/",
        Symbols = true,
        Verbosity = NuGetVerbosity.Detailed,
        Properties = new Dictionary<string, string>
        {
            { "Configuration", configuration },
            { "Platform", "AnyCPU" }
        }
    };

    var projectFilePaths = GetFiles("./src/**/*.csproj").Where(p => !p.FullPath.Contains("Test") && !p.FullPath.Contains("packages") &&!p.FullPath.Contains("tools"));
    foreach(var projectFilePath in projectFilePaths)
    {
        var nuspecFilename = string.Format("{0}/{1}.nuspec", projectFilePath.GetDirectory(),projectFilePath.GetFilenameWithoutExtension());
		if (FileExists(nuspecFilename)) {
			Information("Transforing nuspec file: " + nuspecFilename);
			TransformConfig(nuspecFilename, 
				new TransformationCollection {
					{ "package/metadata/version", version.AssemblySemVer }
				});
		}
        Information("Packaging: " + projectFilePath.FullPath);

        NuGetPack(projectFilePath.FullPath, settings);
    }
});

// Build the DocFX documentation site
Task("Documentation")
    .Does(() =>
{
    DocFxMetadata("./doc/docfx.json");
    DocFxBuild("./doc/docfx.json");

    CreateDirectory("artifacts");
    // Archive the generated site
    ZipCompress("./doc/_site", "./artifacts/Greenshot2.Editor.Api.zip");
});

// Run the XUnit tests via OpenCover, so be get an coverage.xml report
Task("Coverage")
    .IsDependentOn("Build")
    .WithCriteria(() => !BuildSystem.IsLocalBuild)
    .Does(() =>
{
    var openCoverSettings = new OpenCoverSettings() {
        // Forces error in build when tests fail
        ReturnTargetCodeOffset = 0
    };
	if (GetFiles("./**/*.csproj").Any(p => !p.FullPath.Contains("Test"))) {
		Information("Skipping tests, nothing found.");
		return;
	}

    var projectFiles = GetFiles("./**/*.csproj");
    foreach(var projectFile in projectFiles)
    {
        var projectName = projectFile.GetDirectory().GetDirectoryName();
        if (projectName.Contains("Test")) {
           openCoverSettings.WithFilter("-["+projectName+"]*");
        }
        else {
           openCoverSettings.WithFilter("+["+projectName+"]*");
        }
    }

    // Make XUnit 2 run via the OpenCover process
    OpenCover(
        // The test tool Lamdba
        tool => {
            tool.XUnit2("./**/*.Tests.dll",
                new XUnit2Settings {
                    // Add AppVeyor output, this "should" take care of a report inside AppVeyor
                    ArgumentCustomization = args => {
                        if (!BuildSystem.IsLocalBuild) {
                            args.Append("-appveyor");
                        }
                        return args;
                    },
                    ShadowCopy = false,
                    XmlReport = true,
                    HtmlReport = true,
                    ReportName = solutionName,
                    OutputDirectory = "./artifacts",
                    WorkingDirectory = "./src"
                });
            },
        // The output path
        new FilePath("./artifacts/coverage.xml"),
        // Settings
       openCoverSettings
    );
});

// This starts the actual MSBuild
Task("Build")
    .IsDependentOn("RestoreNuGetPackages")
    .IsDependentOn("Clean")
    .IsDependentOn("AssemblyVersion")
    .Does(() =>
{
    var settings = new MSBuildSettings {
        Verbosity = Verbosity.Minimal,
        ToolVersion = MSBuildToolVersion.VS2017,
        Configuration = configuration,
        PlatformTarget = PlatformTarget.MSIL
    };

    MSBuild(solutionFilePath.FullPath, settings);
	
    // Make sure the .dlls in the obj path are not found elsewhere
    CleanDirectories("./**/obj");

    CreateDirectory("artifacts");
	CopyFiles(string.Format("./src/{0}*/**/*.exe", solutionName) , "./artifacts", false);
	CopyFiles(string.Format("./src/{0}*/**/*.exe.config", solutionName) , "./artifacts", false);
});

// Load the needed NuGet packages to make the build work
Task("RestoreNuGetPackages")
    .Does(() =>
{
    NuGetRestore(solutionFilePath.FullPath);
});

// Version is written to the AssemblyInfo files when !BuildSystem.IsLocalBuild
Task("AssemblyVersion")
    .Does(() =>
{
	version = GitVersion(new GitVersionSettings {
        UpdateAssemblyInfo = !BuildSystem.IsLocalBuild
    });
    Information("Version of this build: " + version.AssemblySemVer);
    Information("Nuget Version of this build: " + version.NuGetVersion);
    Information("FullSemVer Version of this build: " + version.FullSemVer);
});


// Clean all unneeded files, so we build on a clean file system
Task("Clean")
    .Does(() =>
{
    CleanDirectories("./**/obj");
    CleanDirectories("./**/bin");
    CleanDirectories("./artifacts");	
});

RunTarget(target);