using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using System.IO.IsolatedStorage;
using System.IO;
using Support;

namespace IE.Debug.Core
{
    public static class WebPageDebugger
    {
        private static WebBrowser browser;
        
        public static string pageHtml
        {
            get { return browser.SaveToString(); }
        }

        public static string pageUri
        {
            get { return String.Empty; }
        }

        public static void Initialize(WebBrowser browser) 
        {
            WebPageDebugger.browser = browser;
        }

        public static void InstallDebugConsole()
        {
            try
            {
                string nativeReady = "(function(){ cordova.require('cordova/channel').onNativeReady.fire()})();";

                browser.InvokeScript("eval", new string[] { Scripting.PhoneGapInjectScript });
                browser.InvokeScript("eval", new string[] { FileUtils.ReadFileContent("app/www/js/wpHtmlDebugger.js") });

                browser.InvokeScript("execScript", new string[] { nativeReady });
            }
            catch (Exception ex)
            {
                Support.Messages.ShowError("Sorry, an error occured. " + ex.Message);
            }
            //browser.InvokeScript("eval", new string[] { Scripting.BuilInjectScript(@"http://mshare.akvelon.net:8184/cordova-init.js")});
        }

        public static void ConnectWeinerDebugger()
        {
            try
            {
                browser.InvokeScript("eval", new string[] { @"window.WeinreServerId='wp7'" });
                browser.InvokeScript("eval", new string[] { @"window.WeinreServerURL='http://debug.shadow.adobe.com:8080'" });

                browser.InvokeScript("eval", new string[] { FileUtils.ReadFileContent(@"app/www/wp-hacks.js") });
                browser.InvokeScript("eval", new string[] { FileUtils.ReadFileContent(@"app/www/target-script-min.js") });

                MessageBox.Show("Debugger connected to the following endpoint: http://debug.shadow.adobe.com:8080/client/#wp7");
            }
            catch (Exception ex) 
            {
                Support.Messages.ShowError("Sorry, an error occured. " + ex.Message);
            }
        }

        public static void InstallFirebug()
        {
            browser.InvokeScript("eval", new string[] { Scripting.FirebugInjectScript });
            //(@"function(F,i,r,e,b,u,g,L,I,T,E){if(F.getElementById(b))return;E=F[i+'NS']&&F.documentElement.namespaceURI;E=E?F[i+'NS'](E,'script'):F[i]('script');E[r]('id',b);E[r]('src',I+g+T);E[r](b,u);(F[e]('head')[0]||F[e]('body')[0]).appendChild(E);E=new%20Image;E[r]('src',I+L);})(document,'createElement','setAttribute','getElementsByTagName','FirebugLite','4','firebug-lite.js','releases/lite/latest/skin/xp/sprite.png','https://getfirebug.com/','#startOpened')();");
        }

        public static void ExecuteCustomScript(string script)
        {
            browser.InvokeScript("eval", new string[] { script});
        }
    }
}
