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
using Microsoft.Phone.Controls;
using System.IO;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using Support;
using IE.Debug.Core;
using IE.Debug.WindowsPhone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Info;

namespace IE.Debug.WindowsPhone.Views
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();            
        }
        

        private void AppbarButtonRefresh_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/ViewSource.xaml", UriKind.Relative));
        }

        private void ActionRunFirebug(object sender, EventArgs e)
        {
            WebPageDebugger.InstallFirebug();             
        }

        private void ActionConnectWeinreDebugger(object sender, EventArgs e)
        {
            WebPageDebugger.ConnectWeinerDebugger();
        }

        private void ActionClearPerfLog(object sender, EventArgs e)
        {
            WebPageDebugger.ExecuteCustomScript("window.profiler.reset();");
        }

        private void ActionGetPerfLog(object sender, EventArgs e)
        {
            WebPageDebugger.ExecuteCustomScript("console.log($dumpProfileJSON (window.profiler));");
        }

        private void ActionInstallDebugConsole(object sender, EventArgs e)
        {
            WebPageDebugger.InstallDebugConsole();
        }

        private void ActionZoomIn(object sender, EventArgs e)
        {
            pvtSource.ZoomIn();
        }

        private void ActionZoomOut(object sender, EventArgs e)
        {
            pvtSource.ZoomOut();
        }

        private void ActionEmailSourceCode(object sender, EventArgs e)
        {
            pvtSource.Email();
        }

        private void Pivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (pivotControl.SelectedItem == null) return;

            var pivotItem = (pivotControl.SelectedItem as PivotItem);

            var control = pivotItem.FindName(pivotItem.Tag.ToString());

            if (control is IPivotItemSelectable)
                (control as IPivotItemSelectable).OnSelected();

            ApplicationBar = (ApplicationBar)Resources[pivotItem.Tag + "ApplicationBar"];
        }
    }
}