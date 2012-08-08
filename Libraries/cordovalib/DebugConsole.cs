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
using System.Diagnostics;

namespace WP7CordovaClassLib.Cordova
{
    public static class DebugConsole
    {
        public static event EventHandler OnWriteLine;

        public static void WriteLine(string message)
        {
            Debug.WriteLine(message);

            if (DebugConsole.OnWriteLine != null)
            {
                OnWriteLine(message, null);
            }
        }
    }
}
