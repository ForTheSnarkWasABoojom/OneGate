using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using OneGate.Backend.Gateway.Shared.Api.Contracts;

namespace OneGate.Backend.Gateway.Shared.Api.Extensions.Validation
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