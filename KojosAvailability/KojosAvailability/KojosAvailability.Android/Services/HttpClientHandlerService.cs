using System.Net.Http;
using KojosAvailability.Droid.Service;
using KojosAvailability.Interfaces;
using Xamarin.Forms;

[assembly: Dependency(typeof(HttpClientHandlerService))]
namespace KojosAvailability.Droid.Service
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