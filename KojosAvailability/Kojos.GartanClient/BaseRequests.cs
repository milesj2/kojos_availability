using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Kojos.GartanClient
{
    internal static class BaseRequests
    {
        static CancellationTokenSource cts = new CancellationTokenSource();
        static HttpClient client;

        public static string BaseUrl { get; set; }

        static BaseRequests()
        {
            client = new HttpClient();
        }

        public async static Task<HttpResponseMessage> Get(string route)
        {
            Uri uri = new Uri(string.Format(BaseUrl + route, string.Empty));

            return await Get(uri);
        }

        public async static Task<HttpResponseMessage> Get(string route, Dictionary<string, string> dictParameters)
        {
            string parameters = FormatParameters(dictParameters);

            try
            {
                Uri uri = new Uri(string.Format(BaseUrl + route + parameters, string.Empty));
                return await Get(uri);
            }
            catch (UriFormatException e)
            {
                throw e;
            }


        }

        public async static Task<HttpResponseMessage> Get(Uri uri)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                return response;
            }
            catch (HttpRequestException e)
            {
                throw e;
            }
            catch (WebException e)
            {
                throw e;
            }
            catch (OperationCanceledException e)
            {
                if (e.CancellationToken == cts.Token)
                {
                    throw e;
                }
                else
                {
                    throw new TimeoutException();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw e;
            }
        }

        public async static Task<HttpResponseMessage> Put(string route, Dictionary<string, string> content)
        {
            string parameters = FormatParameters(content);
            Uri uri = new Uri(string.Format(BaseUrl + route + parameters));
            return await Put(uri, new FormUrlEncodedContent(content));
        }

        public async static Task<HttpResponseMessage> Put(Uri uri, HttpContent content)
        {
            try
            {
                HttpResponseMessage response = await client.PutAsync(uri, null);
                return response;
            }
            catch (HttpRequestException e)
            {
                throw e;
            }
            catch (WebException e)
            {
                throw e;
            }
            catch (OperationCanceledException e)
            {
                if (e.CancellationToken == cts.Token)
                {
                    throw e;
                }
                else
                {
                    throw new TimeoutException();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw e;
            }
        }

        public async static Task<HttpResponseMessage> Post(string route, Dictionary<string, string> content)
        {
            Uri uri = new Uri(string.Format(BaseUrl + route));
            var result = await Post(uri, new FormUrlEncodedContent(content));

            return result;
        }

        public async static Task<HttpResponseMessage> Post(Uri uri, FormUrlEncodedContent content)
        {
            try
            {
                HttpResponseMessage response = await client.PostAsync(uri.ToString(), content);
                return response;
            }
            catch (HttpRequestException e)
            {
                throw e;
            }
            catch (WebException e)
            {
                throw e;
            }
            catch (OperationCanceledException e)
            {
                if (e.CancellationToken == cts.Token)
                {
                    throw e;
                }
                else
                {
                    throw new TimeoutException();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw e;
            }
        }

        public static string FormatParameters(Dictionary<string, string> dictParameters)
        {
            StringBuilder parameters = new StringBuilder("?");
            int i = 0;
            foreach (KeyValuePair<string, string> param in dictParameters)
            {
                i++;
                parameters.Append($"{param.Key}={param.Value}&");
                if (i != dictParameters.Count)
                {
                    parameters.Append("&");
                }
            }
            return parameters.ToString();
        }

        public static void AddHeader(string key, string value)
        {
            if (client.DefaultRequestHeaders.Contains(key))
            {
                RemoveHeader(key);
            }

            client.DefaultRequestHeaders.Add(key, value);
        }

        public static void AddAuthorization(AuthenticationHeaderValue value) => client.DefaultRequestHeaders.Authorization = value;

        public static void RemoveHeader(string key)
        {
            client.DefaultRequestHeaders.Remove(key);
        }

        public static void UpdateHeader(string key, string value)
        {
            RemoveHeader(key);
            AddHeader(key, value);
        }
    }
}
