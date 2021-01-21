using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using OneGate.Shared.ApiModels.Common;

namespace OneGate.Backend.Gateway.Base.Extensions.Validation
{
    public static class ValidationBaseExtensions
    {
        public static IMvcBuilder ConfigureBaseValidator(this IMvcBuilder builder)
        {
            return builder.ConfigureApiBehaviorOptions(p =>
            {
                p.InvalidModelStateResponseFactory = context => new BadRequestObjectResult(new ErrorModel
                {
                    Message = "Invalid request model"
                });
            });
        }
    }
}