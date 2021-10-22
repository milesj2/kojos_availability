using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;

using Foundation;
using KojosAvailability.Interfaces;
using KojosAvailability.iOS.Services;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(HttpClientHandlerService))]
namespace KojosAvailability.iOS.Services
{
    public class HttpClientHandlerService : IHttpClientHandlerService
    {
        public HttpClientHandler GetInsecureHandler()
        {
            HttpClientHandler handler = new HttpClientHandler();

            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
            {
                if (cert.Issuer.Equals("CN=Fireware web CA, OU=Fireware, O=WatchGuard"))
                    return true;
                return errors == System.Net.Security.SslPolicyErrors.None;
            };

            return handler;
        }
    }
}