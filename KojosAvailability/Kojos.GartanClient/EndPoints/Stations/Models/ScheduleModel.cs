using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kojos.GartanClient.EndPoints.Stations.Models
{
    public class ScheduleRow
    {
        public string RowType { get; set; }

        public List<ScheduleItem> ScheduleItems { get; set; }

        public int BrigadeID { get; set; }

    }
    public class ScheduleItem
    {
        public string ID { get; set; }

        public string Content { get; set; }

        public string BackColour { get; set; }

        public string ForeColour { get; set; }
    }
}
