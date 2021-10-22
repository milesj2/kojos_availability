using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kojos.GartanClient
{
    public class UserAuthentication
    {
        public static string Username { get; set; }

        public static string Password { get; set; }

        public static string ServiceCode { get; set; }

        public static string Token { get; set; }

        public static string GetApiKeyAndValue() => $"apiKey={Token}";
    }
}
