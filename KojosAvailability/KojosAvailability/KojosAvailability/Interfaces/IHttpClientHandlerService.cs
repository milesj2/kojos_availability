using System.Net.Http;

namespace KojosAvailability.Interfaces
{
    public interface IHttpClientHandlerService
    {
        HttpClientHandler GetInsecureHandler();
    }
}
