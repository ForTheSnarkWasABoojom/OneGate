using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using OneGate.Shared.ApiModels.Common;
using OneGate.Shared.ApiLibrary.Base.Exceptions;

namespace OneGate.Shared.ApiLibrary.Base
{
    public static class RequestWrapper
    {
        public static async Task<TResponse> PostRequestAsync<TRequest, TResponse>(this Url url, TRequest model = null)
            where TRequest : class
        {
            var task = url.PostJsonAsync(model).ReceiveJson<TResponse>();
            return await FlurlExceptionWrapper(task);
        }

        public static async Task<TResponse> GetRequestAsync<TRequest, TResponse>(this Url url, TRequest model = null)
            where TRequest : class
        {
            var task = url.SetQueryParams(model).GetJsonAsync<TResponse>();
            return await FlurlExceptionWrapper(task);
        }

        private static async Task<TResponse> FlurlExceptionWrapper<TResponse>(Task<TResponse> task)
        {
            try
            {
                return await task;
            }
            catch (FlurlParsingException ex)
            {
                throw new ServerApiException("Can`t parse server response");
            }
            catch (FlurlHttpTimeoutException ex)
            {
                throw new ServerApiException("Timeout exceeded");
            }
            catch (FlurlHttpException ex)
            {
                var error = await ex.GetResponseJsonAsync<ErrorModel>();
                throw new ClientApiException(error.Message);
            }
        }
    }
}