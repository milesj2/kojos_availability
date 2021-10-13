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

        const string EP_AUTHENTICATE = "GetAuthenticationToken";

        const string EP_AUTHENTICATE_DEPRECATED = "GetAuthenticationToken";

        internal static string GetAuthToken(string username, string passwordBase64)
        {
            return string.Empty;
        }

        internal static async Task<GenericResponseModel> GetAuthenticationToken(string username, string password) => await RequestService.Get<GenericResponseModel>($"{EP_AUTHENTICATE_DEPRECATED}");
    }
}
