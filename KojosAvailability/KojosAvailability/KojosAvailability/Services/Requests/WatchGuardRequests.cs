using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace KojosAvailability.Services.Requests
{
    public class WatchGuardRequests
    {

        const string TAG = "WatchGuardRequests";

        BaseRequests _requests;

        public string Url { get => _requests.BaseUrl; }

        public WatchGuardRequests(HttpClientHandler httpClientHandler, string url, bool ignoreSSL)
        {
            _requests = new BaseRequests(httpClientHandler);

            if (url.EndsWith("logon.shtml"))
            {
                _requests.BaseUrl = url.Replace("logon.shtml", string.Empty);
            }
            else
            {
                _requests.BaseUrl = url;
            }
        }

        public async Task<bool> Authenticate(string username, string password)
        {
            var payload = new Dictionary<string, string>
            {
                {"fw_username", username },
                {"fw_password", password },
                {"fw_domain", "Firebox-DB" },
                {"submit", "Login" },
                {"action", "fw_logon" },
                {"fw_logon_type", "logon" },
                {"lang", "en-US" },
                {"redirect", "" }
            };

            HttpResponseMessage response = await _requests.Post("wgcgi.cgi", payload);

            if (!response.IsSuccessStatusCode)
            {
                return false;
            }

            var content = await response.Content.ReadAsStringAsync();

            return content.Contains("success");
        }

    }
}
