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

namespace IE.Debug.Core
{
    public static class Scripting
    {
        public static readonly string PhoneGapLink = @"http://mshare.akvelon.net:8184/cordova-1.5.0.js";
        public static readonly string TestLink = @"http://mshare.akvelon.net:8184/test.js";
        public static readonly string FirebugLink = @"https://getfirebug.com/firebug-lite.js#startOpened";

        public static string BuilInjectScript(string link)
        {
            return String.Format(
                @"var head = document.getElementsByTagName('head')[0]; var sc1 = document.createElement('script');sc1.src = '{0}'; (head || document.body).appendChild(sc1);", 
                link);
        }

        public static string PhoneGapInjectScript 
        {
            get {return BuilInjectScript(PhoneGapLink); }
        }

        public static string FirebugInjectScript
        {
            get { return BuilInjectScript(FirebugLink); }
        }

    }
}
