using System;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OneGate.Backend.Database;
using OneGate.Backend.Rpc;
using OneGate.Backend.Rpc.Services;
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
                    
                    services.AddTransient<ITimeseriesService, Service>();
                    services.AddTransient<IOhlcTimeseriesRepository, OhlcTimeseriesRepository>();
                    services.AddTransient<IValueTimeseriesRepository, ValueTimeseriesRepository>();
                    
                    services.AddMassTransit(x =>
                    {
                        x.UsingRabbitMq((context, cfg) =>
                        {
                            cfg.Host("rabbitmq", "/", h =>
                            {
                                h.Username(Environment.GetEnvironmentVariable("RABBITMQ_DEFAULT_USER"));
                                h.Password(Environment.GetEnvironmentVariable("RABBITMQ_DEFAULT_PASS"));
                            });
                            cfg.ConfigureEndpoints(context);
                        });
                        x.AddConsumer<Consumer>(typeof(ConsumerSettings));
                    });
                    services.AddMassTransitHostedService();
                });
    }
}