using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Kojos.GartanClient;
using Kojos.GartanClient.CommonModels;
using Kojos.GartanClient.EndPoints.Bookings.Models;
using KojosAvailability.Services;

namespace KojosAvailability.Helpers
{
    public static class GartanHelper
    {

        public static async Task<bool> InitialiseGartan(string serviceCode, string username, string password)
        {
            AppSettings.GartanApiUrl = await GartanSingleton.Authentication.GetAPIUrl(serviceCode);
            AppSettings.GartanUsername = username;
            AppSettings.GartanPassword = password;

            return await InitialiseGartan();
        }

        public static async Task<bool> InitialiseGartan()
        {
            GartanSingleton.Url = AppSettings.GartanApiUrl;

            if (string.IsNullOrEmpty(AppSettings.GartanApiUrl) || string.IsNullOrEmpty(AppSettings.GartanApiUrl) || string.IsNullOrEmpty(AppSettings.GartanApiUrl))
                return false;

            return await GartanSingleton.Authentication.Login(AppSettings.GartanUsername, AppSettings.GartanPassword);
        }




        public static async Task<OnCallStatusModel> GetOnCallStatus()
        {
            var onCallStatus = new OnCallStatusModel();

            var timeNow = DateTime.Now;
            var quarterHour = timeNow.Minute - (timeNow.Minute % 15);
            onCallStatus.Time = new DateTime(timeNow.Year, timeNow.Month, timeNow.Day, timeNow.Hour, quarterHour, 0);

            List<UserBookingsModel> userBookings = await GartanSingleton.Bookings.GetUserBookings("G08", "", "", "2", "0");

            foreach (UserBookingsModel booking in userBookings)
            {
                if ((booking.StartDate <= timeNow) && (booking.EndDate > timeNow))
                {
                    onCallStatus.OnCall = false;
                }
            }

            var applianceStatus = await GartanSingleton.Stations.GetApplianceStatuses("G08");

            onCallStatus.OnTheRun = applianceStatus[0].Status == "OK";

            return onCallStatus;
        }

    }
}
