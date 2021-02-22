using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OneGate.Backend.Core.Engines.Api.Client;
using OneGate.Backend.Engines.Fake.Static.Hosted;
using OneGate.Backend.Engines.Shared.Logging;
using OneGate.Backend.Transport.Bus;
using OneGate.Backend.Transport.Bus.Options;

namespace OneGate.Backend.Engines.Fake.Static
{
    public class Program
    {
        private const string RabbitMqOptionsSection = "RabbitMq";
        private const string EnginesApiClientOptionsSection = "InternalApi:Engines";

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
                    
                    // Engines API.
                    var enginesApiClientSection = configuration.GetSection(EnginesApiClientOptionsSection);
                    services.Configure<EnginesApiClientOptions>(enginesApiClientSection);
            
                    services.AddTransient<IEnginesApiClient, EnginesApiClient>();
                    
                    // Ohlc daemon service.
                    services.AddHostedService<FakeOhlcService>();
                }).UseLogging();
    }
}