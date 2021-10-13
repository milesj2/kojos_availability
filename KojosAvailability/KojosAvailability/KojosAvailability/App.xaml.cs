using KojosAvailability.Helpers;
using KojosAvailability.Pages;
using KojosAvailability.Services;
using NuGet.Configuration;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KojosAvailability
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

        protected override async void OnStart()
        {
            if (!await WatchGuardHelper.AuthenticateWatchGuard())
            {
                MainPage = CreateWatchGuardPage();
            }
            //else if (Settings.NotSet(Settings.GartanApiKey))
            //{
            //    MainPage = CreateGartanLoginPage();
            //}
            else
            {
                MainPage = new MainPage();
            }
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        private WatchGuardRegisterPage CreateWatchGuardPage()
        {
            var watchGuardRegisterPage = new WatchGuardRegisterPage();
            watchGuardRegisterPage.RegisterWatchGuardDetails += HandleWatchGuardDetailsSave;
            return watchGuardRegisterPage;
        }

        private async void HandleWatchGuardDetailsSave(object sender, EventArgs e)
        {
            var page = (WatchGuardRegisterPage)sender;

            if (!await WatchGuardHelper.AuthenticateWatchGuard(page.Url, page.Username, page.Password))
            {
                page.SetErrorMessage("Log in failed.");
                return;
            }

            AppSettings.WatchGuardUrl = page.Url;
            AppSettings.WatchGuardUsername = page.Username;
            AppSettings.WatchGuardPassword = page.Password;


            MainPage = new MainPage();
        }

    }
}
