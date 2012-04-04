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
    public static class DebugTraceListener
    {
        public static event EventHandler OnNewTraceLine;

        static DebugTraceListener()
        {
            WP7CordovaClassLib.Cordova.DebugConsole.OnWriteLine += (obj, message) =>
            {

                if (OnNewTraceLine != null)
                    OnNewTraceLine(obj, message);
            };
        }              
    }
}
