using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using IE.Debug.Core;

namespace IE.Debug.WindowsPhone.Controls
{
    public partial class TraceConsoleControl : UserControl, IPivotItemSelectable
    {
        public TraceConsoleControl()
        {
            InitializeComponent();
            this.Loaded += (arg1, arg2) =>
            {
                DebugTraceListener.OnNewTraceLine += (message, nothing) =>
                {
                    Deployment.Current.Dispatcher.BeginInvoke(() =>
                    {
                        tbConsoleOutput.Text += String.Format("{0}{1}", message, Environment.NewLine);
                    });
                };
            };
        }

        public void OnSelected()
        {

        }

        public void OnDeselected()
        {

        }
    }
}
