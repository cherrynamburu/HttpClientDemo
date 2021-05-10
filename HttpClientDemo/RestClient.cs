using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;

namespace HttpClientDemo
{
    /// <summary>
    /// Makes REST requests.
    /// </summary>
    public class RestClient
    {
        private readonly HttpClient _httpClient;

        /// <summary>
        /// Creates the Instance of the RestClient class.
        /// </summary>
        /// <param name="baseAddress">Base address.</param>
        public RestClient(string baseAddress)
        {
            if (baseAddress is null) { throw new ArgumentNullException(nameof(baseAddress)); }

            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(baseAddress),
            };
        }

        /// <summary>
        /// Makes the GET request.
        /// </summary>
        /// <param name="headers">Headers to be sent along with the request.</param>
        /// <param name="requestUri">Request Uri.</param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> GetRequestAsync(
             Dictionary<string, string> headers,
             string requestUri)
        {
            var requestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(requestUri, UriKind.Relative),
            };

            foreach (var header in headers)
            {
                requestMessage.Headers.Add(header.Key, header.Value);
            }

            return await _httpClient.SendAsync(requestMessage);
        }

        /// <summary>
        /// Makes the POST request.
        /// </summary>
        /// <param name="headers">Headers to be sent along with the request.</param>
        /// <param name="body">Data to be sent as Content with the request. </param>
        /// <param name="requestUri">Request Uri.</param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> PostRequestAsync(
            Dictionary<string, string> headers,
            User body,
            string requestUri)
        {
            var requestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(requestUri, UriKind.Relative),
            };

            foreach (var header in headers)
            {
                requestMessage.Headers.Add(header.Key, header.Value);
            }

            requestMessage.Content = new StringContent(
                    JsonConvert.SerializeObject(body),
                    Encoding.UTF8,
                    "application/json");

            return await _httpClient.SendAsync(requestMessage);
        }

        public async Task<HttpResponseMessage> PatchRequestAsync(
            Dictionary<string, string> headers,
            User body,
            string requestUri)
        {
            var requestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Patch,
                RequestUri = new Uri(requestUri, UriKind.Relative),
            };

            foreach (var header in headers)
            {
                requestMessage.Headers.Add(header.Key, header.Value);
            }

            requestMessage.Content = new StringContent(
                    JsonConvert.SerializeObject(body),
                    Encoding.UTF8,
                    "application/json");

            return await _httpClient.SendAsync(requestMessage);
        }

        public async Task<HttpResponseMessage> DeleteRequestAsync(
            Dictionary<string, string> headers,
            string requestUri)
        {
            var requestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri(requestUri, UriKind.Relative),
            };

            foreach (var header in headers)
            {
                requestMessage.Headers.Add(header.Key, header.Value);
            }

            return await _httpClient.SendAsync(requestMessage);
        }

    }
}
