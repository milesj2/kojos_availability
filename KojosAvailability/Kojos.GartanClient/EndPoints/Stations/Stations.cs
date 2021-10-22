using Kojos.GartanClient.EndPoints.Stations.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kojos.GartanClient.EndPoints.Stations
{
    internal static class Stations
    {
        const string ROUTE = "Services/RDS/Stations/StationWebAPI.svc/";

        const string EP_GetApplianceStatus = "GetApplianceStatus";

        internal static async Task<List<ApplianceStatusModel>> GetApplianceStatusesAsync(string callSign)
            => await RequestService.Get<List<ApplianceStatusModel>>($"{ROUTE}{EP_GetApplianceStatus}?{UserAuthentication.GetApiKeyParameter()}&callSign={callSign}");

    }
}
