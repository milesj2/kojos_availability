using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Kojos.GartanClient;
using KojosAvailability.Services;

namespace KojosAvailability.Helpers
{
    public static class GartanHelper
    {

        public static async Task<bool> InitialiseGartan(string serviceCode, string username, string password)
        {
            AppSettings.GartanApiUrl = serviceCode;
            AppSettings.GartanUsername = username;
            AppSettings.GartanPassword = password;

            return await InitialiseGartan();
        }

        public static async Task<bool> InitialiseGartan() => await GartanSingleton.Authentication.Login(AppSettings.GartanApiUrl, AppSettings.GartanUsername, AppSettings.GartanPassword);


    }
}
