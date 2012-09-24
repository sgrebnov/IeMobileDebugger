using IE.Debug.Core;
using IE.Debug.Weinre;
using Microsoft.Phone.Controls;
using System;
using System.Windows.Controls;

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
            WeinreDebugger.RegisterTarget();
        }

        private void ActionInstallDebugConsole(object sender, EventArgs e)
        {
            WebPageDebugger.InstallDebugConsole();
        }

        private void ActionZoomIn(object sender, EventArgs e)
        {
            //pvtSource.ZoomIn();
        }

        private void ActionZoomOut(object sender, EventArgs e)
        {
            //pvtSource.ZoomOut();
        }

        private void ActionEmailSourceCode(object sender, EventArgs e)
        {
            //pvtSource.Email();
        }

        private void Pivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //if (pivotControl.SelectedItem == null) return;

            //var pivotItem = (pivotControl.SelectedItem as PivotItem);

            //var control = pivotItem.FindName(pivotItem.Tag.ToString());

            //if (control is IPivotItemSelectable)
            //    (control as IPivotItemSelectable).OnSelected();

            //ApplicationBar = (ApplicationBar)Resources[pivotItem.Tag + "ApplicationBar"];
        }
    }
}