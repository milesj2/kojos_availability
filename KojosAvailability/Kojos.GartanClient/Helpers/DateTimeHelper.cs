using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kojos.GartanClient.Helpers
{
    public static class DateTimeHelper
    {

        public static string FormatDateTime(DateTime dateTime) => dateTime.ToString("MM-dd-yyyy HH-mm-ss");

    }
}
