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
var solutionName = solutionFilePath.GetDirectory().GetDirectoryName();
// Used to publish coverage report
var coverallsRepoToken = Argument("coverallsRepoToken", EnvironmentVariable("COVERALLS_REPO_TOKEN"))

// Used to store the version, which is needed during the build and the packaging
var version = EnvironmentVariable("APPVEYOR_BUILD_VERSION") ?? "1.0.0";

// Check if we are in a pull request, publishing of packages and coverage should be skipped
var isPullRequest = !string.IsNullOrEmpty(EnvironmentVariable("APPVEYOR_PULL_REQUEST_NUMBER"));

// Check if the commit is marked as release
var isRelease = (EnvironmentVariable("APPVEYOR_REPO_COMMIT_MESSAGE_EXTENDED")?? "").Contains("[release]");

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
    foreach(var pdbFilePath in GetFiles("./**/bin/**/*.pdb").Where(p => !p.FullPath.Contains("Test") && !p.FullPath.Contains("packages") &&!p.FullPath.Contains("tools")))
    {
        Information("Enhancing PDB: " + pdbFilePath.FullPath);
        GitLink(pdbFilePath.FullPath, gitLinkSettings);
    }

    var settings = new DotNetCorePackSettings  
    {
        OutputDirectory = "./artifacts/",
        Configuration = configuration
    };

    var projectFiles = GetFiles("./**/*.csproj").Where(p => !p.FullPath.Contains("Test") && !p.FullPath.Contains("packages") &&!p.FullPath.Contains("tools"));
    foreach(var projectFile in projectFiles)
    {
        Information("Packaging: " + projectFile.FullPath);
        DotNetCorePack(projectFile.FullPath, settings);
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
    CreateDirectory("artifacts");

    var openCoverSettings = new OpenCoverSettings() {
        // Forces error in build when tests fail
        ReturnTargetCodeOffset = 0
    };

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
    foreach(var assemblyInfoFile in  GetFiles("./**/AssemblyInfo.cs").Where(p => p.FullPath.Contains(solutionName))) {
        var assemblyInfo = ParseAssemblyInfo(assemblyInfoFile.FullPath);
        CreateAssemblyInfo(assemblyInfoFile.FullPath, new AssemblyInfoSettings {
            Version = version,
            InformationalVersion = version,
            FileVersion = version,

            CLSCompliant = assemblyInfo.ClsCompliant,
            Company = assemblyInfo.Company,
            ComVisible = assemblyInfo.ComVisible,
            Configuration = assemblyInfo.Configuration,
            Copyright = assemblyInfo.Copyright,
            //CustomAttributes = assemblyInfo.CustomAttributes,
            Description = assemblyInfo.Description,
            //Guid = assemblyInfo.Guid,
            InternalsVisibleTo = assemblyInfo.InternalsVisibleTo,
            Product = assemblyInfo.Product,
            Title = assemblyInfo.Title,
            Trademark = assemblyInfo.Trademark
        });
    }
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