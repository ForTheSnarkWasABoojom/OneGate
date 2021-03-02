using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace OneGate.Backend.Gateway.Shared.Api.Extensions.Swagger
{
    public static class SwaggerBaseExtensions
    {
        public static IServiceCollection AddBaseSwagger(this IServiceCollection services, string apiName, int version)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = $"OneGate Gateway - {apiName} v{version} API",
                    Version = $"v{version}"
                });
                options.OperationFilter<AuthOperationFilter>();
                options.OperationFilter<RemoveVersionOperationFilter>();
                options.DocumentFilter<ReplaceVersionDocumentFilter>();
                options.AddSecurityDefinition("bearer", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT"
                });
                options.EnableAnnotations(true, true);
            });
            services.AddSwaggerGenNewtonsoftSupport();
            return services;
        }

        public static IApplicationBuilder UseBaseSwagger(this IApplicationBuilder app, int version)
        {
            app.UseSwagger(options =>
            {
                options.PreSerializeFilters.Add((swaggerDoc, httpReq) =>
                {
                    swaggerDoc.Servers = new List<OpenApiServer>()
                    {
                        new OpenApiServer
                        {
                            Url = ""
                        }
                    };
                });
            });
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint($"/swagger/v{version}/swagger.json", $"v{version}");
                options.RoutePrefix = "swagger";
            });
            return app;
        }
    }
}