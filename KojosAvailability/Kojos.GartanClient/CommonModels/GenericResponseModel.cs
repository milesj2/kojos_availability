using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kojos.GartanClient.CommonModels
{
    public class GenericResponseModel
    {
        public string Code { get; set; }

        public string Data { get; set; }

        public string EmployeeId { get; set; }

        public string Message { get; set; }
    }
}
