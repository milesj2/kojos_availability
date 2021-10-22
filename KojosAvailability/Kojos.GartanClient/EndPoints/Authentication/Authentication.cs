using Kojos.GartanClient.CommonModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Kojos.GartanClient.EndPoints.Authentication
{
    internal static class Authentication
    {
        const string ROUTE = "Services/RDS/Authentication/AuthenticationWebAPI.svc/";

        const string EP_AUTHENTICATE = "GetAuthToken";

        const string EP_AUTHENTICATE_DEPRECATED = "GetAuthenticationToken";

        internal static async Task<GenericResponseModel> GetAuthToken(string username, string password)
        {
            var bytes = Encoding.UTF8.GetBytes($"{username}:{password}");
            string auth = $"{Convert.ToBase64String(bytes)}";

            GenericResponseModel genericResponseModel = default;

            try
            {
                RequestService.AddAuthorization(new AuthenticationHeaderValue("Basic", auth));
                genericResponseModel = await RequestService.Get<GenericResponseModel>($"{ROUTE}{EP_AUTHENTICATE}");
            }
            catch (HttpRequestException e)
            {

                if (e.StatusCode != System.Net.HttpStatusCode.Unauthorized)
                {
                    return genericResponseModel;
                }

                throw e;
            }

            return genericResponseModel;
        }


        internal static async Task<GenericResponseModel> GetAuthenticationToken(string username, string password) => await RequestService.Get<GenericResponseModel>($"{ROUTE}{EP_AUTHENTICATE_DEPRECATED}");
    }
}
