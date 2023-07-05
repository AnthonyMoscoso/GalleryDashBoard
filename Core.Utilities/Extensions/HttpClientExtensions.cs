using Core.Utilities.Ensures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Extensions
{
    public static class HttpClientExtensions
    {
        public static async Task<HttpResponseMessage> GetAsync(this HttpClient httpClient, string requestUri, IDictionary<string, string>? headers)
        {
            if (headers != null && headers.Any())
            {
                httpClient.AddHeaders(headers);
            }
            return await httpClient.GetAsync(requestUri);
        }
        public static void AddHeaders(this HttpClient httpClient, IDictionary<string, string>? headers = null)
        {
            Ensure.That(httpClient, nameof(httpClient)).IsNotNull();
            if (headers != null && headers.Any())
            {
                HttpHeaders httpHeaders = httpClient.DefaultRequestHeaders;
                foreach (var header in headers)
                {
                    if (httpHeaders.Contains(header.Key))
                    {
                        httpClient.DefaultRequestHeaders.Remove(header.Key);
                    }
                    httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
                }
            }

        }
    }
}
