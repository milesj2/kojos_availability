using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kojos.GartanClient
{
    public static class RequestService
    {
        public static async Task<T> Get<T>(string route)
        {
            var httpResponseMessage = await BaseRequests.Get(route);

            var responseMessage = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
            if (httpResponseMessage.IsSuccessStatusCode && !string.IsNullOrWhiteSpace(responseMessage))
            {
                return JsonConvert.DeserializeObject<T>(responseMessage);
            }
            //throw new HttpClientTaskException(
            //    httpResponseMessage.StatusCode,
            //    string.IsNullOrWhiteSpace(responseMessage)
            //        ? httpResponseMessage.StatusCode.ToString()
            //        : responseMessage);

            return default;
        }

    }
}
