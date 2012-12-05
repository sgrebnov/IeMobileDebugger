namespace IE.Debug.WindowsPhone.Controls
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using IE.Debug.Core;

    /// <summary>
    /// Represents TraceConsole control.
    /// </summary>
    public partial class TraceConsoleControl : UserControl, IPivotItemSelectable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TraceConsoleControl"/> class.
        /// </summary>
        public TraceConsoleControl()
        {
            this.InitializeComponent();
            this.Loaded += (arg1, arg2) =>
            {
                DebugTraceListener.OnNewTraceLine += (message, nothing) =>
                {
                    Deployment.Current.Dispatcher.BeginInvoke(() =>
                    {
                        tbConsoleOutput.Text += string.Format("{0}{1}", message, Environment.NewLine);
                    });
                };
            };
        }

        /// <summary>
        /// Handles pivot item selected event.
        /// </summary>
        public void OnSelected()
        {
        }

        /// <summary>
        /// Handles pivot item deselected event.
        /// </summary>
        public void OnDeselected()
        {
        }
    }
}
