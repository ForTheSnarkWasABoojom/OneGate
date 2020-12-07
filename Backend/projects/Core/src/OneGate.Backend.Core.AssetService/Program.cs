using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OneGate.Backend.Core.AssetService.Repository;
using OneGate.Backend.Database;
using OneGate.Backend.Transport.Bus;

namespace OneGate.Backend.Core.AssetService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddEntityFrameworkNpgsql().AddDbContext<DatabaseContext>();

                    services.AddTransient<IService, Service>();
                    
                    services.AddTransient<IExchangeRepository, ExchangeRepository>();
                    services.AddTransient<IAssetRepository, AssetRepository>();
                    services.AddTransient<ILayoutRepository, LayoutRepository>();

                    // Mass Transit.
                    services.UseMassTransit(new[]
                    {
                        new KeyValuePair<Type, Type>(typeof(Consumer), typeof(ConsumerSettings)),
                    });
                });
    }
}