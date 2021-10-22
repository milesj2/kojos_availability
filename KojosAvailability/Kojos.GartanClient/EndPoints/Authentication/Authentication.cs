using Kojos.GartanClient.CommonModels;
using System;
using System.Collections.Generic;
using System.Linq;
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
            string auth = $"Basic {Convert.ToBase64String(bytes)}";

            GenericResponseModel genericResponseModel = default;

            try
            {
                RequestService.AddHeader("Authorization", auth);
                genericResponseModel = await RequestService.Get<GenericResponseModel>($"{ROUTE}{EP_AUTHENTICATE}");
            }
            finally
            {
                RequestService.RemoveHeader("Authorization");
            }

            return genericResponseModel;
        }


        internal static async Task<GenericResponseModel> GetAuthenticationToken(string username, string password) => await RequestService.Get<GenericResponseModel>($"{ROUTE}{EP_AUTHENTICATE_DEPRECATED}");
    }
}
