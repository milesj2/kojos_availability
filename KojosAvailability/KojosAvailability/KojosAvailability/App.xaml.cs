using KojosAvailability.Helpers;
using KojosAvailability.Interfaces;
using KojosAvailability.Pages;
using KojosAvailability.Resources.Styles;
using KojosAvailability.Services;
using NuGet.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KojosAvailability
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new LoadingScreen();
        }

        protected override async void OnStart()
        {
            await Initialise();
        }

        protected override void OnSleep()
        {
        }

        protected override async void OnResume()
        {
            await Initialise();
        }

        private async Task Initialise()
        {
            HandleThemes();

            if (!await WatchGuardHelper.AuthenticateWatchGuard())
            {
                MainPage = CreateWatchGuardPage();
            }
            else if (!await GartanHelper.InitialiseGartan())
            {
                MainPage = CreateGartanRegisterPage();
            }
            else
            {
                MainPage = new MainPage();
            }
        }

        private void HandleThemes()
        {
            ICollection<ResourceDictionary> mergedDictionaries = Current.Resources.MergedDictionaries;

            if (mergedDictionaries != null)
            {
                mergedDictionaries.Clear();

                var theme = DependencyService.Get<IEnvironment>().GetOperatingSystemTheme();

                switch (theme)
                {
                    case Theme.Dark:
                        mergedDictionaries.Add(new DarkTheme());
                        break;
                    case Theme.Light:
                    default:
                        mergedDictionaries.Add(new LightTheme());
                        break;
                }
            }
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

            if (!await GartanHelper.InitialiseGartan())
            {
                MainPage = CreateGartanRegisterPage();
            }
            else
            {
                MainPage = new MainPage();
            }
        }

        private GartanRegisterPage CreateGartanRegisterPage()
        {
            var gartanRegisterPage = new GartanRegisterPage();
            gartanRegisterPage.RegisterGartanDetails += HandleGartanDetailsSave;
            return gartanRegisterPage;
        }

        private async void HandleGartanDetailsSave(object sender, EventArgs e)
        {
            var page = (GartanRegisterPage)sender;

            if (!await GartanHelper.InitialiseGartan(page.ServiceCode, page.Username, page.Password))
            {
                page.SetErrorMessage("Log in failed.");
                return;
            }

            MainPage = new MainPage();
        }

    }
}
