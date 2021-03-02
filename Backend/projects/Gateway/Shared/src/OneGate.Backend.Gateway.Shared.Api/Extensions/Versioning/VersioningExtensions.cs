using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace OneGate.Backend.Gateway.Shared.Api.Extensions.Versioning
{
    public static class VersioningExtensions
    {
        public static IServiceCollection AddBaseVersioning(this IServiceCollection services, int defaultVersion)
        {
            services.AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(defaultVersion, 0);
                options.ReportApiVersions = true;
            });
            return services;
        }    
    }
}