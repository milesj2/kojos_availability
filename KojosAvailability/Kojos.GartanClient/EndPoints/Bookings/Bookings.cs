using Kojos.GartanClient.EndPoints.Bookings.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kojos.GartanClient.EndPoints.Bookings
{
    internal class Bookings
    {
        const string ROUTE = "Services/RDS/Bookings/BookingWebAPI.svc/";

        const string EP_GET_USER_BOOKINGS = "GetUserBookings";

        internal static async Task<List<UserBookingsModel>> GetUserBookings(
            string stationCallSign,
            string startDate,
            string endDate,
            string maximumRows,
            string startRowIndex)
        {
            var values = new Dictionary<string, string>
            {
                { "apiKey", UserAuthentication.Token },
                { "stationCallSign", stationCallSign },
                { "startDate", startDate },
                { "endDate", endDate },
                { "maximumRows", maximumRows },
                { "startRowIndex", startRowIndex },
            };

            return await RequestService.Get<List<UserBookingsModel>>($"{ROUTE}{EP_GET_USER_BOOKINGS}", values);
        }

    }
}
