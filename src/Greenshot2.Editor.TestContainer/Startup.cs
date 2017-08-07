//  Greenshot - a free and open source screenshot tool
//  Copyright (C) 2007-2017 Thomas Braun, Jens Klingen, Robin Krom
// 
//  For more information see: http://getgreenshot.org/
//  The Greenshot project is hosted on GitHub: https://github.com/greenshot
// 
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 1 of the License, or
//  (at your option) any later version.
// 
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
// 
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Windows;
using Dapplo.CaliburnMicro.Dapp;
using Dapplo.Log;
using Dapplo.Log.Loggers;

namespace Greenshot2.Editor.TestContainer
{
    public static class Startup
    {
        /// <summary>
        /// Entry point for the application
        /// </summary>
        [STAThread]
        public static void Main()
        {
#if DEBUG
            LogSettings.RegisterDefaultLogger<DebugLogger>(LogLevels.Verbose);
#endif
            var dapplication = new Dapplication("Greenshot2.Editor", "147710FF-6F58-4F7D-A100-5D298E311AAE")
            {
                ShutdownMode = ShutdownMode.OnLastWindowClose,
                // Don't allow the application to run multiple times
                OnAlreadyRunning = () =>
                {
                    MessageBox.Show("Greenshot 2 editor already running.", "Greenshot 2 editor", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return -1;
                },
                ObserveUnhandledTaskException = true,
                OnUnhandledTaskException = exception => new LogSource().Error().WriteLine(exception.Message)
            };

            // Add the DLL's we need
            dapplication.Bootstrapper.FindAndLoadAssemblies("Greenshot2.Editor*");


            // Let Dapplo initialize everything, including the web-app
            dapplication.Run();
        }
    }
}

