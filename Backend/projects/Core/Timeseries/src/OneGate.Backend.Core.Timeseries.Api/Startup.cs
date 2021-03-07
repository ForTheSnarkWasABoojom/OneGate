using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OneGate.Backend.Core.Shared.Api.Extensions.Swagger;
using OneGate.Backend.Core.Shared.Api.Extensions.Versioning;
using OneGate.Backend.Core.Shared.Api.Middleware;
using OneGate.Backend.Core.Shared.Database;
using OneGate.Backend.Core.Timeseries.Api.Mapping;
using OneGate.Backend.Core.Timeseries.Database;
using Prometheus;

namespace OneGate.Backend.Core.Timeseries.Api
{
    public class Startup
    {
        private const int ApiVersion = 1;
        private const string ApiTitle = "Timeseries";

        private const string DatabaseConnectionOptionsSection = "DatabaseConnection";

        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration.GetSection("OneGate");
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // Newtonsoft as serializer.
            services.AddControllers(p =>
                {
                    p.Filters.Add<ExceptionFilter>();
                })
                .AddNewtonsoftJson();

            // Versioning.
            services.AddBaseVersioning(ApiVersion);

            // Enforce to use lowercase.
            services.AddRouting(options => options.LowercaseUrls = true);

            // Swagger.
            services.AddBaseSwagger(ApiTitle, ApiVersion);

            // Automapper.
            services.AddAutoMapper(p =>
                p.AddProfile<MappingProfile>()
            );

            // Database.
            var dbConfiguration = _configuration.GetSection(DatabaseConnectionOptionsSection);
            var dbOptions = dbConfiguration.Get<DatabaseConnectionOptions>();

            var connectionString = ConnectionString.Build(dbOptions);
            services.AddDbContext<DatabaseContext>(p => p.UseNpgsql(connectionString));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Routing.
            app.UseRouting();

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