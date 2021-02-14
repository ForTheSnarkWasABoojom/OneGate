using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OneGate.Backend.Gateway.Base.Extensions.Authentication;
using OneGate.Backend.Gateway.Base.Extensions.Swagger;
using OneGate.Backend.Gateway.Base.Extensions.Validation;
using OneGate.Backend.Gateway.Base.Extensions.Versioning;
using OneGate.Backend.Gateway.Base.Middleware;
using OneGate.Backend.Gateway.Base.Options;
using OneGate.Backend.Gateway.UserApi.Mapping;
using OneGate.Backend.Transport.Bus;
using OneGate.Backend.Transport.Bus.Options;
using Prometheus;

namespace OneGate.Backend.Gateway.UserApi
{
    public class Startup
    {
        private const int ApiVersion = 1;
        private const string ApiTitle = "User";

        private readonly IConfiguration _configuration;
        private const string AuthenticationOptionsSection = "Authentication";
        private const string RabbitMqOptionsSection = "RabbitMq";

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration.GetSection("OneGate");
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // Newtonsoft as serializer.
            services.AddControllers()
                .ConfigureBaseValidator()
                .AddNewtonsoftJson();

            // Versioning.
            services.AddBaseVersioning(ApiVersion);

            // Authentication.
            var authenticationSection = _configuration.GetSection(AuthenticationOptionsSection);

            services.AddBaseAuthentication(authenticationSection.Get<AuthenticationOptions>());
            services.Configure<AuthenticationOptions>(authenticationSection);

            // Enforce to use lowercase.
            services.AddRouting(options => options.LowercaseUrls = true);

            // Swagger.
            services.AddBaseSwagger(ApiTitle, ApiVersion);

            // MassTransit.
            var rabbitMqSection = _configuration.GetSection(RabbitMqOptionsSection);
            services.UseTransportBus(rabbitMqSection.Get<RabbitMqOptions>());

            // Automapper.
            services.AddAutoMapper(p =>
                p.AddProfile<MappingProfile>()
            );
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Exception handler.
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseMiddleware<ErrorHandlingMiddleware>();
            }

            // Routing.
            app.UseRouting();

            // Authorization.
            app.UseAuthentication();
            app.UseAuthorization();

            // Prometheus metrics.
            app.UseHttpMetrics();

            // Endpoints.
            app.UseEndpoints(endpoints =>
            {
                // Api.
                endpoints.MapControllers();
                // Prometheus.
                endpoints.MapMetrics("/metrics");
            });

            // Swagger.
            app.UseBaseSwagger(ApiVersion);
        }
    }
}