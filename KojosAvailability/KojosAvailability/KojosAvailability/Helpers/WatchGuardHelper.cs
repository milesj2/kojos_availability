using KojosAvailability.Interfaces;
using KojosAvailability.Services;
using KojosAvailability.Services.Requests;
using NuGet.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace KojosAvailability.Helpers
{
    public static class WatchGuardHelper
    {

        public static WatchGuardRequests CreateNewInsecureInstance(string url) =>
            new WatchGuardRequests(DependencyService.Get<IHttpClientHandlerService>().GetInsecureHandler(), url, true);

        public static async Task<bool> AuthenticateWatchGuard()
        {
            if (string.IsNullOrEmpty(AppSettings.WatchGuardUrl) || string.IsNullOrEmpty(AppSettings.WatchGuardUsername) || string.IsNullOrEmpty(AppSettings.WatchGuardPassword))
                return false;

            return await AuthenticateWatchGuard(AppSettings.WatchGuardUrl, AppSettings.WatchGuardUsername, AppSettings.WatchGuardPassword);
        }

        public static async Task<bool> AuthenticateWatchGuard(string url, string username, string password) => await CreateNewInsecureInstance(url).Authenticate(username, password);

    }
}
