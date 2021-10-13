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
    public partial class GartanRegisterPage : ContentPage
    {

        public event EventHandler RegisterGartanDetails;

        public string ServiceCode
        {
            get
            {
                return txtServiceCode.Text;
            }
        }

        public string Username
        {
            get
            {
                return txtGartanUsername.Text;
            }
        }

        public string Password
        {
            get
            {
                return txtGartanPassword.Text;
            }
        }

        public GartanRegisterPage()
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
            if (string.IsNullOrEmpty(txtServiceCode.Text)) return;
            if (string.IsNullOrEmpty(txtGartanUsername.Text)) return;
            if (string.IsNullOrEmpty(txtGartanPassword.Text)) return;

            RegisterGartanDetails?.Invoke(this, EventArgs.Empty);
        }
    }
}