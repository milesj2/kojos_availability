using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kojos.GartanClient
{
    public class UserAuthentication
    {
        //private static UserAuthentication _userAuthentication;

        //public static UserAuthentication GetInstance()
        //{
        //    if (_userAuthentication == null)
        //    {
        //        _userAuthentication = new UserAuthentication();
        //    }

        //    return _userAuthentication;
        //}

        public static string Username { get; set; }

        public static string Password { get; set; }

        public static string ServiceCode { get; set; }

        public static string Token { get; set; }

        public static string GetApiKeyParameter() => $"apiKey={Token}";
    }
}
