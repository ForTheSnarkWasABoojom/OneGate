using System;
using System.Threading.Tasks;
using Flurl.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using OneGate.Backend.Core.Shared.Api.Contracts;
using OneGate.Shared.ApiModels.Base;

namespace OneGate.Backend.Gateway.Shared.Middleware
{
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        private readonly ILogger<ExceptionFilter> _logger;

        public ExceptionFilter(ILogger<ExceptionFilter> logger)
        {
            _logger = logger;
        }

        public override async Task OnExceptionAsync(ExceptionContext context)
        {
            var error = context.Exception switch
            {
                // Flurl exception.
                FlurlHttpTimeoutException ex => await FromFlurlTimeoutException(ex),
                // Flurl exception.
                FlurlHttpException ex => await FromFlurlException(ex),
                // Undefined exception.
                { } ex => await FromFatalException(ex)
            };

            context.Result = new JsonResult(error)
            {
                StatusCode = error.StatusCode
            };
            context.ExceptionHandled = true;
        }
        
        private async Task<ErrorResponse> FromFlurlTimeoutException(FlurlHttpTimeoutException exception)
        {
            _logger.LogError(exception, "Timeout occured");
            return new ErrorResponse
            {
                StatusCode = 504, 
                Message = "Timeout exceeded"
            };
        }

        private async Task<ErrorResponse> FromFlurlException(FlurlHttpException exception)
        {
            ErrorDto errorDto;
            
            if(!exception.StatusCode.HasValue)
                return await FromFatalException(exception);
            
            try
            {
                errorDto = await exception.GetResponseJsonAsync<ErrorDto>();
            }
            catch (Exception ex)
            {
                return await FromFatalException(ex);
            }
            
            return new ErrorResponse
            {
                StatusCode = errorDto.StatusCode, 
                Message = errorDto.Message
            };
        }

        private async Task<ErrorResponse> FromFatalException(Exception exception)
        {
            _logger.LogError(exception, "Fatal error occured");
            return new ErrorResponse
            {
                StatusCode = 500, 
                Message = "Fatal error"
            };
        }
    }
}