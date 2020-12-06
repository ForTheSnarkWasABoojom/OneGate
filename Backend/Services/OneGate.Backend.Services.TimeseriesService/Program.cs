using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OneGate.Backend.Database;
using OneGate.Backend.Rpc;
using OneGate.Backend.Services.TimeseriesService.Repository;

namespace OneGate.Backend.Services.TimeseriesService
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

                    // Mass Transit.
                    services.UseMassTransit(new []
                    {
                        new KeyValuePair<Type, Type>(typeof(Consumer), typeof(ConsumerSettings)),
                    });
                });
    }
}