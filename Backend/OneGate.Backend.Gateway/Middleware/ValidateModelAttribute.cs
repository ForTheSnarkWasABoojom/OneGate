using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using OneGate.Shared.Models;
using OneGate.Shared.Models.Common;

namespace OneGate.Backend.Gateway.Middleware
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = new UnprocessableEntityObjectResult(new ErrorDto
                {
                    Message = "Request model is not correct"
                });
            }
        }
    }
}