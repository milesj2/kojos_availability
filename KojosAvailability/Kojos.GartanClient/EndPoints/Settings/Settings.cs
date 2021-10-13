using Kojos.GartanClient.CommonModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kojos.GartanClient.EndPoints.Settings
{
    public static class Settings
    {

        const string ROUTE = "Services/RDS/Stations/StationWebAPI.svc/";

        const string EP_SHOW_LEAVE_TOOL = "ShowLeaveTool";

        internal static async Task<GenericResponseModel> ShowLeaveTool() => await RequestService.Get<GenericResponseModel>($"{ROUTE}{EP_SHOW_LEAVE_TOOL}?{UserAuthentication.GetApiKeyParameter()}");

    }
}
