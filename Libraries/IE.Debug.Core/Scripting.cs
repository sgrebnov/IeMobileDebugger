using Support;
using System;

namespace IE.Debug.Core
{
    public static class Scripting
    {
        public static readonly string FirebugLink = @"https://getfirebug.com/firebug-lite.js#startOpened";

        public static string BuilInjectScript(string link)
        {
            return String.Format(
                @"var head = document.getElementsByTagName('head')[0]; var sc1 = document.createElement('script');sc1.src = '{0}'; (head || document.body).appendChild(sc1);", 
                link);
        }

        public static string BuilInjectInlineScript(string text)
        {
            return String.Format(
                @"var head = document.getElementsByTagName('head')[0]; var sc1 = document.createElement('script');sc1.innerHTML = '{0}';sc1.type = 'text/javascript'; (head || document.body).appendChild(sc1);",
                text);
        }

        public static string PhoneGapInjectScript 
        {
            get { return FileUtils.ReadFileContent(@"app/www/cordova-2.0.0.js"); }
        }

        public static string FirebugInjectScript
        {
            get { return BuilInjectScript(FirebugLink); }
        }

    }
}
