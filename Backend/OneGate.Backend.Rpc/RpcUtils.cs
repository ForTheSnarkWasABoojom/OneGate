using System;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;
using EasyNetQ;
using EntityFramework.Exceptions.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using OneGate.Backend.Rpc.Contracts.Base;

namespace OneGate.Backend.Rpc
{
    public static class RpcUtils
    {
        //TODO Re-try policies
        public static async Task<TResponse> CallAsync<TRequest, TResponse>(this IBus bus, TRequest request)
            where TRequest : class
            where TResponse : class
        {
            try
            {
                ResponseBase payload;
                while (true)
                {
                    try
                    {
                        payload = await bus.RequestAsync<TRequest, ResponseBase>(request);
                        break;
                    }
                    catch (TimeoutException)
                    {
                        await Task.Run(() => Thread.Sleep(500));
                    }
                }

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
            catch (UniqueConstraintException ex)
            {
                return new ErrorResponse
                {
                    StatusCode = StatusCodes.Status409Conflict,
                    Message = "Element must be unique",
                    InnerExceptionMessage = ex.Message
                };
            }
            catch (ReferenceConstraintException ex)
            {
                return new ErrorResponse
                {
                    StatusCode = StatusCodes.Status424FailedDependency,
                    Message = "Element has wrong dependencies",
                    InnerExceptionMessage = ex.Message
                };
            }
            catch (DbUpdateException ex)
            {
                return new ErrorResponse
                {
                    StatusCode = StatusCodes.Status422UnprocessableEntity,
                    Message = "Database error",
                    InnerExceptionMessage = ex.ToString()
                };
            }
            catch (DbException ex)
            {
                return new ErrorResponse
                {
                    StatusCode = StatusCodes.Status503ServiceUnavailable,
                    Message = "Database server error",
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