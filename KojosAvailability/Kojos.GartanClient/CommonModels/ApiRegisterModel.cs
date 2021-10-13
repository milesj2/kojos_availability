using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kojos.GartanClient.CommonModels
{
    public class ApiRegisterModel
    {
        public string AccessCode { get; set; }

        public string ApplicationName { get; set; }

        public bool AutoLogin { get; set; }

        public string Name { get; set; }

        public bool TwoFactorAuth { get; set; }

        public string URL { get; set; }

        public bool useOATH { get; set; }

        // "idaAuthority": null,
        // "idaClientID": null,
        // "idaRedirectURI": null,
        // "idaResourceId": null,
    }
}
