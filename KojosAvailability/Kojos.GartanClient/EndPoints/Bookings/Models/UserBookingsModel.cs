using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kojos.GartanClient.EndPoints.Bookings.Models
{
    public class UserBookingsModel
    {
        public string BookingCode { get; set; }

        public string BookingCodeDescription { get; set; }

        public DateTime BookingDate { get; set; }

        public string EmployeeName { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string ServiceNumber { get; set; }

        public string StationCallSign { get; set; }

    }
}
