using Microsoft.Extensions.Hosting;
using Serilog;

namespace OneGate.Backend.Gateway.Base.Extensions.Logging
{
    public static class SerilogExtensions
    {
        public static IHostBuilder UseBaseLogging(this IHostBuilder host)
        {
            host.UseSerilog((hostingContext, services, loggerConfiguration) => loggerConfiguration
                .ReadFrom.Configuration(hostingContext.Configuration)
                .Enrich.FromLogContext()
                .WriteTo.Console());
            return host;
        }
    }
}