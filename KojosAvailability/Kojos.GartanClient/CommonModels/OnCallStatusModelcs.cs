using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kojos.GartanClient.CommonModels
{
    public class OnCallStatusModel
    {

        public DateTime Time { get; set; }

        public bool OnCall { get; set; } = true;

        public bool OnTheRun { get; set; } = true;

    }
}
