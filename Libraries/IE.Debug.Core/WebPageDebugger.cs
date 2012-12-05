namespace IE.Debug.Core
{
    using System;
    using Microsoft.Phone.Controls;
    using Support;

    /// <summary>
    /// Implements basic debugging functionality  
    /// </summary>
    public static class WebPageDebugger
    {                
        /// <summary>
        /// Gets html page content
        /// </summary>
        public static string PageHtml
        {
            get 
            { 
                return Browser.SaveToString(); 
            }
        }

        /// <summary>
        /// Gets page Uri
        /// </summary>
        public static string PageUri
        {
            get
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Gets or sets WebBrower control
        /// </summary>
        private static WebBrowser Browser
        {
            get;
            set;
        }

        /// <summary>
        /// Initializes page debugger
        /// </summary>
        /// <param name="browser">WebBrowser control</param>
        public static void Initialize(WebBrowser browser) 
        {
            WebPageDebugger.Browser = browser;
        }

        /// <summary>
        /// Installs debug console
        /// </summary>
        /// <returns>the value indicating if debug console was installed successfully</returns>
        public static bool InstallDebugConsole()
        {
            try
            {
                Browser.InvokeScript("eval", new string[] { FileUtils.ReadFileContent("app/www/js/console.js") });

                if (string.Compare("true", (string)Browser.InvokeScript("eval", new string[] { FileUtils.ReadFileContent("app/www/js/compatModeDetection.js") })) == 0)
                {
                    Messages.ShowError("Oops. It seems the page runs in compatibility mode. Please fix and try again.");
                    return false;
                }

                Browser.InvokeScript("eval", new string[] { Scripting.PhoneGapInjectScript });
                Browser.InvokeScript("eval", new string[] { FileUtils.ReadFileContent("app/www/js/wpHtmlDebugger.js") });

                string nativeReady = "(function(){ cordova.require('cordova/channel').onNativeReady.fire()})();";
                Browser.InvokeScript("execScript", new string[] { nativeReady });

                return true;
            }
            catch (Exception ex)
            {
                Support.Messages.ShowError("Sorry, an error occured. " + ex.Message);
            }

            return false;
        }

        /// <summary>
        /// Injects Firebug script
        /// </summary>
        public static void InstallFirebug()
        {
            Browser.InvokeScript("eval", new string[] { Scripting.FirebugInjectScript });
            //(@"function(F,i,r,e,b,u,g,L,I,T,E){if(F.getElementById(b))return;E=F[i+'NS']&&F.documentElement.namespaceURI;E=E?F[i+'NS'](E,'script'):F[i]('script');E[r]('id',b);E[r]('src',I+g+T);E[r](b,u);(F[e]('head')[0]||F[e]('body')[0]).appendChild(E);E=new%20Image;E[r]('src',I+L);})(document,'createElement','setAttribute','getElementsByTagName','FirebugLite','4','firebug-lite.js','releases/lite/latest/skin/xp/sprite.png','https://getfirebug.com/','#startOpened')();");
        }

        /// <summary>
        /// Executes custom java script code in WebBrowser control
        /// </summary>
        /// <param name="script">script to execute</param>
        public static void ExecuteCustomScript(string script)
        {
            Browser.InvokeScript("eval", new string[] { script });
        }
    }
}
