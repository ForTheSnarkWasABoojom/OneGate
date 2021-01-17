using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OneGate.Backend.Core.Records.Database;
using OneGate.Backend.Core.Records.Database.Models;
using OneGate.Backend.Core.Records.Database.Repository;
using OneGate.Backend.Transport.Bus;
using OneGate.Shared.ApiContracts.Exchange;

namespace OneGate.Backend.Core.Records.Node
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
                    
                    // Migration.
                    Migrate(services);

                    // Mass Transit.
                    services.UseMassTransit(new[]
                    {
                        new KeyValuePair<Type, Type>(typeof(Consumer), typeof(ConsumerSettings)),
                    });
                });
        
        private static void Migrate(IServiceCollection services)
        {
            using var db = services.BuildServiceProvider().GetService<DatabaseContext>();

            Thread.Sleep(3000);
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
            
            var fakeExchange = new Exchange
            {
                Title = "FAKE",
                Description = "Fake exchange",
                EngineType = EngineTypeDto.FAKE.ToString()
            };
            
            var fakeIndex = new IndexAsset
            {
                Ticker = "OG",
                Country = "Russia",
                Exchange = fakeExchange,
                Description = "Fake index"
            };

            db.Exchanges.Add(fakeExchange);
            db.Assets.Add(fakeIndex);
            
            db.SaveChanges();
        }
    }
}