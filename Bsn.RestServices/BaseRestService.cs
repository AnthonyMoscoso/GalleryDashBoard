using Bsn.RestServices.Enums;
using Bsn.RestServices.Models;
using Bsn.Utilities;
using Bsn.Utilities.Constants;
using Core.Utilities.Ensures;
using Core.Utilities.Extensions;

using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;

namespace Bsn.RestServices
{
    public class BaseRestService : IRest
    {
        public HttpClient HttpClient { get; set; }

        public BaseRestService(HttpClient httpClient)
        {
            HttpClient = httpClient;
        }

        public async Task<RestResult> Get(string url,string token= "", IDictionary<string, string>? headers = null)
        {
            Ensure.That(HttpClient, nameof(HttpClient)).IsNotNull();
            HttpResponseMessage httpResponse;
            RestResult webResult = new();
            if (!string.IsNullOrWhiteSpace(token))
            {
                HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constant.Bearer, token);
            }
            httpResponse = await HttpClient!.GetAsync(url, headers);

            bool isUnathorized = httpResponse.StatusCode.IsUnathorized();
            Ensure.That(isUnathorized).IsFalse(new UnauthorizedAccessException(ErrorMessages.Session_Expired));
            webResult.HttpStatusCode = httpResponse.StatusCode;
            webResult.Result = await httpResponse.Content.ReadAsStringAsync();
            return webResult;
        }

        public async Task<RestResult> Send<T>(string url, T objectRequest, RequestMethods requestMethod = RequestMethods.Post, string token = "", IDictionary<string, string>? headers = null)
        {
            Ensure.That(HttpClient, nameof(HttpClient)).IsNotNull();
            if (!string.IsNullOrWhiteSpace(token))
            {
                HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constant.Bearer, token);
            }
            RestResult webResult = new();
            HttpResponseMessage httpResponse;
            //HttpClient.AddHeaders(headers);
            switch (requestMethod)
            {
                case RequestMethods.Delete:
                    httpResponse = await HttpClient.DeleteAsync(url + $"/{objectRequest!}");
                    break;
                case RequestMethods.Put:
                    httpResponse = await HttpClient.PutAsJsonAsync(url, objectRequest);
                    break;
                case RequestMethods.Patch:
                    StringContent requestContent = new(objectRequest.SerializeInJson(), Encoding.UTF8, "application/json-patch+json");
                    httpResponse = await HttpClient.PatchAsync(url, requestContent);
                    break;
                default:
                    httpResponse = await HttpClient.PostAsJsonAsync(url, objectRequest);
                    break;
            }
            webResult.HttpStatusCode = httpResponse.StatusCode;
            webResult.Result = await httpResponse.Content.ReadAsStringAsync();
            return webResult;
        }

   
      
    }
}
