using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OneGate.Common.Models.Common;

namespace OneGate.Frontend.ApiLibrary.Utils
{
    public static class HttpUtils
    {
        private static readonly HttpClient Client = new HttpClient();

        private static async Task<ErrorDto> ReadErrorAsync(HttpResponseMessage response)
        {
            try
            {
                return await response.Content.ReadAsAsync<ErrorDto>();
            }
            catch (Exception)
            {
                throw new OneGateApiException("Unknown error");
            }
        }

        private static async Task<TResponse> ReadContentAsync<TResponse>(HttpRequestMessage httpRequest,
            string accessToken)
        {
            if (!string.IsNullOrEmpty(accessToken))
            {
                httpRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            }

            var response = await Client.SendAsync(httpRequest);
            if (!response.IsSuccessStatusCode)
            {
                var errorDto = await ReadErrorAsync(response);
                throw new OneGateApiException(errorDto.Message);
            }

            try
            {
                return await response.Content.ReadAsAsync<TResponse>();
            }
            catch (Exception)
            {
                throw new OneGateApiException("Wrong response");
            }
        }

        private static string ToQueryString(this object obj)
        {
            if (obj == null)
                return string.Empty;

            return string.Join("&", obj.GetType()
                .GetProperties()
                .Where(x => Attribute.IsDefined(x, typeof(FromQueryAttribute)) && x.GetValue(obj) != null)
                .Select(x =>
                    $"{Uri.EscapeDataString((Attribute.GetCustomAttribute(x, typeof(FromQueryAttribute)) as FromQueryAttribute).Name)}" +
                    $"={Uri.EscapeDataString(x.GetValue(obj).ToString())}"));
        }

        public static async Task<TResponse> PostAsync<TRequest, TResponse>(Uri baseEndpoint, string accessToken,
            string methodName, TRequest bodyModel, object queryModel = null)
        {
            var endpointUri = new Uri(baseEndpoint, $"{methodName}?{ToQueryString(queryModel)}");
            var httpRequest = new HttpRequestMessage
            {
                Content = new ObjectContent<TRequest>(bodyModel, new JsonMediaTypeFormatter()),
                Method = HttpMethod.Post,
                RequestUri = endpointUri
            };

            return await ReadContentAsync<TResponse>(httpRequest, accessToken);
        }

        public static async Task<TResponse> GetAsync<TResponse>(Uri baseEndpoint, string accessToken,
            string methodName, object queryModel = null)
        {
            var endpointUri = new Uri(baseEndpoint, $"{methodName}?{ToQueryString(queryModel)}");
            var httpRequest = new HttpRequestMessage
            {
                Content = null,
                Method = HttpMethod.Get,
                RequestUri = endpointUri
            };
            return await ReadContentAsync<TResponse>(httpRequest, accessToken);
        }

        public static async Task DeleteAsync(Uri baseEndpoint, string accessToken,
            string methodName, object queryModel = null)
        {
            var endpointUri = new Uri(baseEndpoint, $"{methodName}?{ToQueryString(queryModel)}");
            var httpRequest = new HttpRequestMessage
            {
                Content = null,
                Method = HttpMethod.Delete,
                RequestUri = endpointUri
            };
            await ReadContentAsync<object>(httpRequest, accessToken);
        }
    }
}