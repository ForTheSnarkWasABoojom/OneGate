﻿using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OneGate.Backend.Transport.Bus;
using OneGate.Common.Models.Common;

namespace OneGate.Backend.Gateway.Middleware
{
    [AllowAnonymous]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : ControllerBase
    {
        [Route("error")]
        public IActionResult ExceptionHandler()
        {
            var exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;
            return exception switch
            {
                ApiException ex => StatusCode(ex.StatusCode, new ErrorDto()
                {
                    Message = ex.Message,
                    Exception = Environment.GetEnvironmentVariable("API_DISPLAY_EXCEPTIONS") == "TRUE" ? ex.InnerExceptionMessage : null
                }),
                Exception ex => StatusCode(StatusCodes.Status500InternalServerError, new ErrorDto()
                {
                    Message = ex.Message,
                    Exception = Environment.GetEnvironmentVariable("API_DISPLAY_EXCEPTIONS") == "TRUE" ? ex.ToString() : null
                }),
                _ => StatusCode(StatusCodes.Status500InternalServerError, new ErrorDto()
                {
                    Message = "Unknown error"
                })
            };
        }
    }
}