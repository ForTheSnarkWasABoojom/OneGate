using System;
using System.Data.Common;
using System.Threading.Tasks;
using EasyNetQ;
using Microsoft.AspNetCore.Http;
using OneGate.Backend.Rpc.Contracts.Base;

namespace OneGate.Backend.Rpc
{
    public static class RpcUtils
    {
        public static async Task<TResponse> CallAsync<TRequest, TResponse>(this IBus bus, TRequest request)
            where TRequest : class
            where TResponse : class
        {
            try
            {
                var payload = await bus.RequestAsync<TRequest, ResponseBase>(request);

                if (payload is ErrorResponse errorResponse)
                    throw new ApiException(errorResponse.Message, errorResponse.StatusCode,
                        errorResponse.InnerExceptionMessage);

                if (!(payload is TResponse result))
                    throw new ApiException("Unknown response format");
                return result;
            }
            catch (EasyNetQException ex)
            {
                throw new ApiException(ex.Message, 500, ex.ToString());
            }
        }

        public static IDisposable RegisterMethodAsync<TRequest, TResponse>(this IBus bus,
            Func<TRequest, Task<TResponse>> action)
            where TRequest : class
            where TResponse : class
        {
            return bus.RespondAsync<TRequest, ResponseBase>(async (request) =>
                await ResponderWrapperAsync(request, action));
        }

        private static async Task<ResponseBase> ResponderWrapperAsync<TRequest, TResponse>(TRequest request,
            Func<TRequest, Task<TResponse>> action)
            where TRequest : class
            where TResponse : class
        {
            try
            {
                return await action(request) as ResponseBase;
            }
            catch (ApiException ex)
            {
                return new ErrorResponse
                {
                    StatusCode = ex.StatusCode,
                    Message = ex.Message,
                    InnerExceptionMessage = ex.InnerExceptionMessage
                };
            }
            catch (DbException ex)
            {
                return new ErrorResponse
                {
                    StatusCode = StatusCodes.Status503ServiceUnavailable,
                    Message = "Database error",
                    InnerExceptionMessage = ex.ToString()
                };
            }
            catch (Exception ex)
            {
                return new ErrorResponse()
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = ex.Message,
                    InnerExceptionMessage = ex.ToString()
                };
            }
        }
    }
}