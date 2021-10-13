using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Kojos.GartanClient
{
    class HttpRequestException : Exception
    {
        public string ResponseMessage { get; }

        public string RequestUri { get; }

        public Uri BaseAddress { get; }

        public HttpStatusCode StatusCode { get; }

        public HttpRequestException(HttpStatusCode httpStatusCode, string message) : base(message) => StatusCode = httpStatusCode;

        public HttpRequestException(string responseMessage, string requestUri, Uri baseAddress, HttpStatusCode statusCode, Exception innerException = null)
            : base($"Status code {statusCode} returned from {string.Concat(baseAddress, requestUri)} with a message of {responseMessage}", innerException)
        {
            ResponseMessage = responseMessage;
            RequestUri = requestUri;
            BaseAddress = baseAddress;
            StatusCode = statusCode;
        }

        public override string ToString() => Message;
    }
}

