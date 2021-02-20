using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using OneGate.Backend.Gateway.Shared.Extensions.Logging;

namespace OneGate.Backend.Gateway.User.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args)
                .Build()
                .Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); })
                .UseLogging();
    }
}