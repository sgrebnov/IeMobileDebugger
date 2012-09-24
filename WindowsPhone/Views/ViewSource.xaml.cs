using IE.Debug.Core;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Tasks;
using System;

namespace IE.Debug.WindowsPhone
{
    public partial class ViewSource : PhoneApplicationPage
    {
        string html = String.Empty;
        string uri = string.Empty;

        public ViewSource()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {

            this.html = WebPageDebugger.PageHtml;
            this.uri = WebPageDebugger.PageUri;

            this.txtSource.Text = this.html;


            base.OnNavigatedTo(e);

        }

        private void ApplicationBarIconButton_Click(object sender, EventArgs e)
        {
            //in-real world application user expect to select it from his contacts and if not found enter manually.
            EmailComposeTask emailComposeTask = new EmailComposeTask();
            emailComposeTask.Body = html;
            emailComposeTask.Subject = uri;
            emailComposeTask.Show(); 
        }

        private void ApplicationBarIconButton_Click_1(object sender, EventArgs e)
        {
            txtSource.FontSize = Math.Min(txtSource.FontSize + 2, 54);
        }

        private void ApplicationBarIconButton_Click_2(object sender, EventArgs e)
        {
            txtSource.FontSize = Math.Max(txtSource.FontSize - 2, 0);
        }
    }
}