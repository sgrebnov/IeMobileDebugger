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
using Support;

namespace IE.Debug.WindowsPhone.Controls
{
    public partial class ViewSourceControl : UserControl, IPivotItemSelectable
    {
        public ViewSourceControl()
        {
            InitializeComponent();
        }

        private void LoadCurrentHtmlSource()
        {
            tbSource.Text = WebPageDebugger.PageHtml;
        }

        public void ZoomIn()
        {
            tbSource.FontSize = Math.Min(tbSource.FontSize + 2, 54);
        }

        public void ZoomOut()
        {
            tbSource.FontSize = Math.Max(tbSource.FontSize - 2, 0);
        }

        public void Email()
        {
            MessageBox.Show("Not implemented yet");
        }

        public void OnSelected()
        {
            LoadCurrentHtmlSource();
        }

        public void OnDeselected()
        {

        }
    }


}
