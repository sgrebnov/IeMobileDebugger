namespace IE.Debug.WindowsPhone.Controls
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using IE.Debug.Core;
    using Microsoft.Phone.Controls;
    using Support;    
    
    /// <summary>
    /// Represents PageHost control.
    /// </summary>
    public partial class PageHostControl : UserControl, IPivotItemSelectable
    {               
        /// <summary>
        /// Initializes a new instance of the <see cref="PageHostControl"/> class.
        /// </summary>
        public PageHostControl()
        {
            this.InitializeComponent();

            this.Loaded += (arg1, arg2) => 
            {
                txtUrl.InputScope = new InputScope { Names = { new InputScopeName { NameValue = InputScopeNameValue.Url } } };

                WebPageDebugger.Initialize(this.Browser);

                Browser.NavigationFailed += (s, ex) =>
                {
                    if (ex.Exception != null)
                    {
                        Messages.ShowError(ex.Exception.Message);
                    }
                };

                Browser.LoadCompleted += (s, ex) =>
                {
                    if (WebPageDebugger.InstallDebugConsole())
                    {
                        Weinre.WeinreDebugger.RegisterTarget();
                    }
                };

                Browser.Navigate(new Uri(txtUrl.Text, UriKind.RelativeOrAbsolute));

                VisualStateManager.GoToState(this, this.EnableFullscreen ? "Fullscreen" : "Standard", true);
            };
        }

        /// <summary>
        /// Gets or sets a value indicating whether full screen mode is enabled or not
        /// </summary>
        public bool EnableFullscreen
        {
            get;
            set;
        }

        /// <summary>
        /// Gets WebBrowser control.
        /// </summary>
        public WebBrowser Browser
        {
            get { return PGView.Browser; }
        }

        /// <summary>
        /// Occurs when pivot item is selected.
        /// </summary>
        public void OnSelected()
        {
        }

        /// <summary>
        /// Occurs when pivot item is deselected.
        /// </summary>
        public void OnDeselected()
        {
        }

        /// <summary>
        /// Handles Debug button click.
        /// </summary>
        /// <param name="sender">Debug button.</param>
        /// <param name="e">event args.</param>
        private void BtnNavigateClick(object sender, RoutedEventArgs e)
        {
            this.Navigate();
        }        

        /// <summary>
        /// Handles KeyDown event.
        /// </summary>
        /// <param name="sender">input control.</param>
        /// <param name="e">event args.</param>
        private void TxtUrlKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                this.Focus();
                this.Navigate();
            }
        }

        /// <summary>
        /// Navigates to the chosen URL.
        /// </summary>
        private void Navigate()
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
    }
}
