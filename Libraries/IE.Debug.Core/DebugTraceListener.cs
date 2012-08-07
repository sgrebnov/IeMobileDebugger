using System;

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
