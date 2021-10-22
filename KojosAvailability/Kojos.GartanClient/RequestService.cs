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
        public static async Task<T> Get<T>(string route, Dictionary<string, string> values) => await Get<T>(route + BaseRequests.FormatParameters(values));

        public static async Task<T> Get<T>(string route)
        {
            var httpResponseMessage = await BaseRequests.Get(route);

            var responseMessage = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
            if (httpResponseMessage.IsSuccessStatusCode && !string.IsNullOrWhiteSpace(responseMessage))
            {
                return JsonConvert.DeserializeObject<T>(responseMessage);
            }
            throw new HttpRequestException(
                httpResponseMessage.StatusCode,
                string.IsNullOrWhiteSpace(responseMessage)
                    ? httpResponseMessage.StatusCode.ToString()
                    : responseMessage);
        }

        public static void AddHeader(string key, string value) => BaseRequests.AddHeader(key, value);

        public static void RemoveHeader(string key) => BaseRequests.RemoveHeader(key);


    }
}
