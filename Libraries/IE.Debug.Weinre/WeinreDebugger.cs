using IE.Debug.Core;
using Support;
using System;
using System.IO.IsolatedStorage;
using System.Windows;

namespace IE.Debug.Weinre
{
    /// <summary>
    /// Implements weinre debugger injection functionality
    /// </summary>
    public static class WeinreDebugger
    {
        /// <summary>
        /// Weinre server
        /// </summary>
        private static readonly string WeinreServerURL = @"http://debug.shadow.adobe.com:8080";
        
        /// <summary>
        /// Injects weinre debugger script to the page
        /// </summary>
        public static void RegisterTarget()
        {
            try
            {
                // install cross domain request wrapper
                WebPageDebugger.ExecuteCustomScript(@"window.wpHtmlDebugger.applyXSSpatch()");

                // weinre settings
                WebPageDebugger.ExecuteCustomScript(String.Format(@"window.WeinreServerId='{0}'", WeinreId));
                WebPageDebugger.ExecuteCustomScript(String.Format(@"window.WeinreServerURL='{0}'", WeinreServerURL));
                
                // run weinre script
                WebPageDebugger.ExecuteCustomScript(FileUtils.ReadFileContent(@"app/www/wp-hacks.js")); 
                WebPageDebugger.ExecuteCustomScript(FileUtils.ReadFileContent(@"app/www/target-script-min.js"));

                MessageBox.Show(String.Format("Successfully connected! Use the following uri in desktop browser: {0}/client/#{1}", WeinreServerURL, WeinreId));
            }
            catch (Exception ex)
            {
                Support.Messages.ShowError("Sorry, an error occured. " + ex.Message);
            }
        }

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
        /// Generates unique id for target device
        /// </summary>
        /// <returns></returns>
        private static string GenerateWeinreId()
        {
            return Guid.NewGuid().ToString().Substring(0, 4);
        }
    }
}
