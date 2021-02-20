using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using OneGate.Backend.Engines.Shared.Logging;
using OneGate.Backend.Transport.Bus;
using OneGate.Backend.Transport.Bus.Options;

namespace OneGate.Backend.Engines.Fake.Static
{
    public class Program
    {
        private const string RabbitMqOptionsSection = "RabbitMq";
        
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    // Configuration.
                    var configuration = hostContext.Configuration.GetSection("OneGate");
                    
                    // Mass Transit.
                    var rabbitMqSection = configuration.GetSection(RabbitMqOptionsSection);
                    services.UseTransportBus(rabbitMqSection.Get<RabbitMqOptions>());
                    
                    // services.AddHostedService<DaemonService>();
                }).UseLogging();
    }
}