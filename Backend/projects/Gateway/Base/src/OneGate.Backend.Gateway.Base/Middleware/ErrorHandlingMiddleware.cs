using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OneGate.Backend.Transport.Bus;
using OneGate.Backend.Transport.Bus.Exceptions;
using OneGate.Shared.ApiModels.Base;
using TransportException = OneGate.Backend.Transport.Bus.Exceptions.TransportException;

namespace OneGate.Backend.Gateway.Base.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var errorModel = new ErrorModel();

            switch (ex)
            {
                // Exception driven development. Errors represented as exceptions.
                case RemoteException rpcException:
                    errorModel.StatusCode = rpcException.StatusCode;
                    errorModel.Message = rpcException.Message;
                    break;
                // Transport timeout exception.
                case TransportTimeoutException transportTimeoutException:
                    errorModel.StatusCode = 504;
                    errorModel.Message = "Request timeout";
                    _logger.LogError(ex, "Request timeout occured");
                    break;
                // Common transport exception.
                case TransportException transportException:
                    errorModel.StatusCode = 500;
                    errorModel.Message = "Internal error";
                    _logger.LogError(ex, "Transport error occured");
                    break;
                // Unknown exception.
                default:
                    errorModel.StatusCode = 500;
                    errorModel.Message = "Unknown error";
                    _logger.LogError(ex, "Unknown error occured");
                    break;
            }

            var result = JsonConvert.SerializeObject(errorModel);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = errorModel.StatusCode;

            return context.Response.WriteAsync(result);
        }
    }
}