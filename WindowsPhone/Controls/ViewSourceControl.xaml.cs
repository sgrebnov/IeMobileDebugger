namespace IE.Debug.WindowsPhone.Controls
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using IE.Debug.Core;

    /// <summary>
    /// Represents ViewSource control.
    /// </summary>
    public partial class ViewSourceControl : UserControl, IPivotItemSelectable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ViewSourceControl"/> class.
        /// </summary>
        public ViewSourceControl()
        {
            this.InitializeComponent();
        }        

        /// <summary>
        /// Zooms in
        /// </summary>
        public void ZoomIn()
        {
            tbSource.FontSize = Math.Min(tbSource.FontSize + 2, 54);
        }

        /// <summary>
        /// Zooms out
        /// </summary>
        public void ZoomOut()
        {
            tbSource.FontSize = Math.Max(tbSource.FontSize - 2, 0);
        }

        /// <summary>
        /// Sends email
        /// </summary>
        public void Email()
        {
            MessageBox.Show("Not implemented yet");
        }

        /// <summary>
        /// Handles pivot item selected event.
        /// </summary>
        public void OnSelected()
        {
            this.LoadCurrentHtmlSource();
        }

        /// <summary>
        /// Handles pivot item deselected event.
        /// </summary>
        public void OnDeselected()
        {
        }

        /// <summary>
        /// Loads current html page
        /// </summary>
        private void LoadCurrentHtmlSource()
        {
            tbSource.Text = WebPageDebugger.PageHtml;
        }
    }
}
