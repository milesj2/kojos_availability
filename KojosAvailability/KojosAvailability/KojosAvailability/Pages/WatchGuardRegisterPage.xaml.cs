using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KojosAvailability.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WatchGuardRegisterPage : ContentPage
    {

        public event EventHandler RegisterWatchGuardDetails;

        public string Url
        {
            get
            {
                return txtWatchGuardUrl.Text;
            }
        }

        public string Username
        {
            get
            {
                return txtWatchGuardUsername.Text;
            }
        }

        public string Password
        {
            get
            {
                return txtWatchGuardPassword.Text;
            }
        }

        public WatchGuardRegisterPage()
        {
            InitializeComponent();

        }

        public void SetErrorMessage(string error)
        {
            if (!string.IsNullOrEmpty(error))
            {
                lblError.Text = error;
                lblError.IsVisible = true;
            }
        }

        private void btnSave_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtWatchGuardUrl.Text) || !Uri.IsWellFormedUriString(txtWatchGuardUrl.Text, UriKind.Absolute)) return;
            if (string.IsNullOrEmpty(txtWatchGuardUsername.Text)) return;
            if (string.IsNullOrEmpty(txtWatchGuardPassword.Text)) return;

            RegisterWatchGuardDetails?.Invoke(this, EventArgs.Empty);
        }
    }
}