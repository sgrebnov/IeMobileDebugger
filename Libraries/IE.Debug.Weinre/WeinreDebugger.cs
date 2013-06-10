namespace IE.Debug.Weinre
{
    using System;
    using System.IO.IsolatedStorage;
    using System.Windows;
    using IE.Debug.Core;
    using Support;

    /// <summary>
    /// Implements weinre debugger injection functionality.
    /// </summary>
    public static class WeinreDebugger
    {
        /// <summary>
        /// Weinre server.
        /// </summary>
        private static readonly string WeinreServerURL = @"http://debug.edgeinspect.adobe.com";

        /// <summary>
        /// Gets unique identifier for this device.
        /// </summary>
        private static string WeinreId
        {
            get
            {
                var settings = IsolatedStorageSettings.ApplicationSettings;

                if (!settings.Contains("WeinreId"))
                {
                    settings["WeinreId"] = GenerateWeinreId();
                    settings.Save();
                }

                return settings["WeinreId"] as string;
            }
        }

        /// <summary>
        /// Injects weinre debugger script to the page.
        /// </summary>
        public static void RegisterTarget()
        {
            try
            {
                // install cross domain request wrapper
                WebPageDebugger.ExecuteCustomScript(@"window.wpHtmlDebugger.applyXSSpatch()");

                // weinre settings
                WebPageDebugger.ExecuteCustomScript(string.Format(@"window.WeinreServerId='{0}'", WeinreId));
                WebPageDebugger.ExecuteCustomScript(string.Format(@"window.WeinreServerURL='{0}'", WeinreServerURL));
                
                // run weinre script
                WebPageDebugger.ExecuteCustomScript(FileUtils.ReadFileContent(@"app/www/wp-hacks.js")); 
                WebPageDebugger.ExecuteCustomScript(FileUtils.ReadFileContent(@"app/www/target-script-min.js"));

                MessageBox.Show(string.Format("Success! Open the following uri in desktop browser: {0}/client/#{1}", WeinreServerURL, WeinreId));
            }
            catch (Exception ex)
            {
                Support.Messages.ShowError("Sorry, an error occured. " + ex.Message);
            }
        }       

        /// <summary>
        /// Generates unique id for target device.
        /// </summary>
        /// <returns>Weinre identifier.</returns>
        private static string GenerateWeinreId()
        {
            return Guid.NewGuid().ToString().Substring(0, 4);
        }
    }
}
