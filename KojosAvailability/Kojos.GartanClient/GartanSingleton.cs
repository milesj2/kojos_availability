using Kojos.GartanClient.CommonModels;
using Kojos.GartanClient.EndPoints.Bookings.Models;
using Kojos.GartanClient.EndPoints.Settings;
using Kojos.GartanClient.EndPoints.Stations.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Kojos.GartanClient
{
    public static class GartanSingleton
    {

        public static string Url
        {
            get
            {
                return BaseRequests.BaseUrl;
            }
            set
            {
                BaseRequests.BaseUrl = value;
            }
        }

        static GartanSingleton()
        {
            // ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            BaseRequests.AddHeader("AppKey", "GTL646878");
        }

        private static async Task<T> MethodWrapperAsync<T>(
            Func<Task<T>> f,
            bool checkedToken = false,
            CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(UserAuthentication.Token))
            {
                try
                {
                    UserAuthentication.Token = await GetToken();
                    // if (UserAuthentication.Token == null) throw;
                }
                catch (HttpRequestException httpException)
                {
                    throw;
                }
            }

            try
            {
                return await f.Invoke();
            }
            catch (HttpRequestException httpException)
            {
                if (httpException.StatusCode == HttpStatusCode.Unauthorized)
                {
                    UserAuthentication.Token = await GetToken();

                    return await MethodWrapperAsync(f, true, cancellationToken);
                }

                throw;
            }
        }

        private static async Task<string> GetToken()
        {
            if (string.IsNullOrEmpty(UserAuthentication.Username) || string.IsNullOrEmpty(UserAuthentication.Password))
            {
                return null;
            }

            GenericResponseModel response = await EndPoints.Authentication.Authentication.GetAuthToken(UserAuthentication.Username, UserAuthentication.Password);
            return response.Data;
        }

        public static class Authentication
        {
            public static bool LoggingIn { get; private set; }

            public static async Task<bool> Login(string username, string password)
            {
                LoggingIn = true;

                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password)) return false;
                UserAuthentication.Username = username;
                UserAuthentication.Password = password;

                if (string.IsNullOrEmpty(Url)) return false;

                try
                {
                    UserAuthentication.Token = await GetToken();
                    LoggingIn = false;
                    return true;
                }
                catch (HttpRequestException httpException)
                {
                    LoggingIn = false;
                    if (httpException.StatusCode == HttpStatusCode.Unauthorized) return false;
                    throw;
                }


            }

            internal static async Task<bool> ValidateSession()
            {
                try
                {
                    await Settings.ShowLeaveTool();

                    return true;
                }
                catch (HttpRequestException httpException)
                {
                    if (httpException.StatusCode == HttpStatusCode.Unauthorized) return false;

                    throw;
                }
            }

            public static async Task<string> GetAPIUrl(string serviceCode)
            {
                BaseRequests.BaseUrl = string.Empty;
                var response = await RequestService.Get<ApiRegisterModel>($"https://messaging.gartantech.com/DeviceManager.svc/register?accessCode={serviceCode}");
                return response.URL;
            }
        }

        public static class Bookings
        {
            public static async Task<List<UserBookingsModel>> GetUserBookings(
                string stationCallSign,
                string startDate,
                string endDate,
                string maximumRows,
                string startRowIndex)
                => await MethodWrapperAsync(() => EndPoints.Bookings.Bookings.GetUserBookings(stationCallSign, startDate, endDate, maximumRows, startRowIndex));
        }

        public static class Stations
        {
            public static async Task<List<ApplianceStatusModel>> GetApplianceStatuses(string stationCallSign)
                => await MethodWrapperAsync(() => EndPoints.Stations.Stations.GetApplianceStatusesAsync(stationCallSign));
        }
    }
}
