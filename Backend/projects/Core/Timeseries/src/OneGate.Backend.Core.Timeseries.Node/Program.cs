using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OneGate.Backend.Core.Timeseries.Database;
using OneGate.Backend.Core.Timeseries.Database.Repository;
using OneGate.Backend.Transport.Bus;

namespace OneGate.Backend.Core.Timeseries.Node
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
                    
                    services.AddTransient<IOhlcSeriesRepository, OhlcSeriesRepository>();
                    services.AddTransient<IPointSeriesRepository, PointSeriesRepository>();
                    
                    // Migration.
                    Migrate(services);

                    // Mass Transit.
                    services.UseMassTransit(new []
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
        }
    }
}