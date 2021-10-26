using Kojos.GartanClient.EndPoints.Stations.Models;
using Kojos.GartanClient.Enums;
using Kojos.GartanClient.Helpers;
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

        const string EP_GETAPPLIANCESTATUS = "GetApplianceStatus";
        const string EP_GETSCHEDULE = "GetSchedule";

        internal static async Task<List<ApplianceStatusModel>> GetApplianceStatusesAsync(string callSign)
            => await RequestService.Get<List<ApplianceStatusModel>>($"{ROUTE}{EP_GETAPPLIANCESTATUS}?{UserAuthentication.GetApiKeyAndValue()}&callSign={callSign}");

        internal static async Task<List<ScheduleRow>> GetSchedule(
            string brigadeId,
            DateTime bookingStart,
            DateTime bookingEnd,
            Resolution resolution,
            bool header = false,
            bool employees = true,
            bool rules = false,
            int empBrigID = 0)
        {

            var values = new Dictionary<string, string>()
            {
                {nameof(brigadeId), brigadeId.ToString() },
                {nameof(bookingStart), DateTimeHelper.FormatDateTime(bookingStart) },
                {nameof(bookingEnd), DateTimeHelper.FormatDateTime(bookingEnd) },
                {nameof(resolution), resolution.ToString() },
                {nameof(header), header.ToString() },
                {nameof(employees), employees.ToString() },
                {nameof(rules), rules.ToString() },
                {nameof(empBrigID), empBrigID.ToString() },
            };

            return await RequestService.Get<List<ScheduleRow>>($"{ROUTE}{EP_GETSCHEDULE}", values);

        }

    }
}
