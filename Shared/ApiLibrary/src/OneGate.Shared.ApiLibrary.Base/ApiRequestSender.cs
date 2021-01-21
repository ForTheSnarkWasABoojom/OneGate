using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using OneGate.Shared.ApiModels.Common;
using OneGate.Shared.ApiLibrary.Base.Exceptions;

namespace OneGate.Shared.ApiLibrary.Base
{
    public static class ApiRequestWrapper
    {
        public static async Task<TResponse> PostRequestAsync<TRequest, TResponse>(this Url url, TRequest model)
        {
            try
            {
                return await url.PostJsonAsync(model).ReceiveJson<TResponse>();
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