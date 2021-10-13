using Kojos.GartanClient.CommonModels;
using Kojos.GartanClient.EndPoints.Settings;
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

        internal static string Url
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
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
        }

        private static async Task<T> MethodWrapperAsync<T>(
            Func<Task<T>> f,
            bool checkedToken = false,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (string.IsNullOrEmpty(UserAuthentication.Token))
            {
                UserAuthentication.Token = await GetToken();
            }

            try
            {
                return await f.Invoke();
            }
            catch (HttpRequestException httpException)
            {
                if (httpException.StatusCode == System.Net.HttpStatusCode.Unauthorized)
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
                // throw error
            }

            GenericResponseModel response = await EndPoints.Authentication.Authentication.GetAuthenticationToken(UserAuthentication.Username, UserAuthentication.Password);
            return response.Data;
        }

        public static class Authentication
        {
            public static async Task<bool> Login(string serviceCode, string username, string password)
            {
                if (string.IsNullOrEmpty(serviceCode) || string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password)) return false;
                UserAuthentication.Username = username;
                UserAuthentication.Password = password;
                Url = string.Empty;
                try
                {
                    Url = await GetAPIUrl(serviceCode);
                }
                catch
                {
                    throw;
                }

                if (string.IsNullOrEmpty(Url)) return false;

                return await ValidateSession();
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

            private static async Task<string> GetAPIUrl(string serviceCode)
            {
                var response = await RequestService.Get<ApiRegisterModel>($"https://messaging.gartantech.com/DeviceManager.svc/register?accessCode={serviceCode}");
                return response.URL;
            }
        }
    }
}
