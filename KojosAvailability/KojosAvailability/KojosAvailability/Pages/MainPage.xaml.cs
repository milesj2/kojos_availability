using KojosAvailability.Helpers;
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
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var status = await GartanHelper.GetOnCallStatus();

            string onCall = status.OnCall ? string.Empty : "not";

            lblMain.Text = $"You are {onCall} on call";
        }

    }
}