using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using OneGate.Backend.Core.Shared.Api.Contracts;

namespace OneGate.Backend.Core.Shared.Api.Middleware
{
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        private readonly ILogger<ExceptionFilter> _logger;

        public ExceptionFilter(ILogger<ExceptionFilter> logger)
        {
            _logger = logger;
        }

        public override void OnException(ExceptionContext context)
        {
            var error = new ErrorDto();
            switch (context.Exception)
            {
                // Unknown exception.
                default:
                    error.StatusCode = 500;
                    error.Message = "Unknown error";
                    _logger.LogError(context.Exception, "Unknown error occured");
                    break;
            }

            context.Result = new JsonResult(error)
            {
                StatusCode = error.StatusCode
            };
            context.ExceptionHandled = true;
        }
    }
}