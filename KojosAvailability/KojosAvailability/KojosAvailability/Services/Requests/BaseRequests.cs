using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KojosAvailability.Services.Requests
{
    internal class BaseRequests
    {
        protected CancellationTokenSource cts = new CancellationTokenSource();

        public HttpClient Client { get; }

        public string BaseUrl;

        public BaseRequests(HttpClientHandler handlerService)
        {
            Client = new HttpClient(handlerService);
            Client.Timeout = TimeSpan.FromSeconds(30);
        }

        #region Get
        public async Task<HttpResponseMessage> Get(string route)
        {
            Uri uri = new Uri(string.Format(BaseUrl + route, string.Empty));

            return await Get(uri);
        }

        public async Task<HttpResponseMessage> Get(string route, Dictionary<string, string> dictParameters)
        {
            string parameters = FormatParameters(dictParameters);

            Uri uri = new Uri(string.Format(BaseUrl + route + parameters, string.Empty));

            return await Get(uri);
        }

        public async Task<HttpResponseMessage> Get(Uri uri)
        {
            try
            {
                HttpResponseMessage response = await Client.GetAsync(uri);
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

        #endregion

        #region Put

        public async Task<HttpResponseMessage> Put(string route, Dictionary<string, string> content)
        {
            string parameters = FormatParameters(content);
            Uri uri = new Uri(string.Format(BaseUrl + route + parameters));
            return await Put(uri, new FormUrlEncodedContent(content));
        }

        public async Task<HttpResponseMessage> Put(Uri uri, HttpContent content)
        {
            try
            {
                HttpResponseMessage response = await Client.PutAsync(uri, null);
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

        #endregion

        #region Post

        public async Task<HttpResponseMessage> Post(string route, Dictionary<string, string> content)
        {
            Uri uri = new Uri(string.Format(BaseUrl + route), UriKind.Absolute);
            try
            {
                var result = await Post(uri, new FormUrlEncodedContent(content));

                return result;
            }
            catch
            {
                throw;
            }


        }

        public async Task<HttpResponseMessage> Post(Uri uri, FormUrlEncodedContent content)
        {
            try
            {
                HttpResponseMessage response = await Client.PostAsync(uri, content);

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
            catch (UriFormatException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw e;
            }
        }

        #endregion

        private string FormatParameters(Dictionary<string, string> dictParameters)
        {
            StringBuilder parameters = new StringBuilder("?");
            int i = 0;
            foreach (KeyValuePair<string, string> param in dictParameters)
            {
                i++;
                if (i != dictParameters.Count)
                    parameters.Append(param.Key + "=" + param.Value + "&");
                else
                    parameters.Append(param.Key + "=" + param.Value);
            }
            return parameters.ToString();
        }

        public void AddHeader(string key, string value)
        {
            Client.DefaultRequestHeaders.Add(key, value);
        }

        public void RemoveHeader(string key)
        {
            Client.DefaultRequestHeaders.Remove(key);
        }

        public void UpdateHeader(string key, string value)
        {
            RemoveHeader(key);
            AddHeader(key, value);
        }
    }
}
