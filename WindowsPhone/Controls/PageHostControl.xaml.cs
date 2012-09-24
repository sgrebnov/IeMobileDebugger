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
using Support;
using Microsoft.Phone.Controls;
using IE.Debug.Core;

namespace IE.Debug.WindowsPhone.Controls
{
    public partial class PageHostControl : UserControl, IPivotItemSelectable
    {
        public WebBrowser Browser { 
            get { return PGView.Browser; } 
        }
        
        public PageHostControl()
        {
            InitializeComponent();

            this.Loaded += (arg1, arg2) => {

                txtUrl.InputScope = new InputScope { Names = { new InputScopeName { NameValue = InputScopeNameValue.Url } } };

                WebPageDebugger.Initialize(this.Browser);

                Browser.NavigationFailed += (s, ex) =>
                {
                    if (ex.Exception != null) {
                        Messages.ShowError(ex.Exception.Message);
                    }
                };

                Browser.LoadCompleted += (s, ex) =>
                {
                    WebPageDebugger.InstallDebugConsole();
                    Weinre.WeinreDebugger.RegisterTarget();
                };

                Browser.Navigate(new Uri(txtUrl.Text, UriKind.RelativeOrAbsolute));
            };
        }

        private void btnNavigate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var uri = new Uri(txtUrl.Text, UriKind.Absolute);

                PGView.Browser.Navigate(uri);
            }
            catch (Exception ex)
            {
                Messages.ShowError(ex.Message);

            }
        }

        public void OnSelected()
        {
            
        }

        public void OnDeselected()
        {
            
        }
    }
}
