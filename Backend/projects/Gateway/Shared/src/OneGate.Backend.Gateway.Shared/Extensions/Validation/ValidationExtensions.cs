using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using OneGate.Shared.ApiModels.Base;

namespace OneGate.Backend.Gateway.Shared.Extensions.Validation
{
    public static class ValidationBaseExtensions
    {
        public static IMvcBuilder ConfigureBaseValidator(this IMvcBuilder builder)
        {
            return builder.ConfigureApiBehaviorOptions(p =>
            {
                p.InvalidModelStateResponseFactory = context => new BadRequestObjectResult(new ErrorResponse
                {
                    Message = "Invalid request model"
                });
            });
        }
    }
}