using Microsoft.Extensions.Hosting;
using Serilog;

namespace OneGate.Backend.Core.Shared.Logging
{
    public static class SerilogExtensions
    {
        public static IHostBuilder UseLogging(this IHostBuilder host)
        {
            host.UseSerilog((hostingContext, services, loggerConfiguration) => loggerConfiguration
                .ReadFrom.Configuration(hostingContext.Configuration)
                .Enrich.FromLogContext()
                .WriteTo.Console());
            return host;
        }
    }
}