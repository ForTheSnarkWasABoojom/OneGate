using System;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OneGate.Backend.Database;

namespace OneGate.Backend.Engines.FakeEngine
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
                    services.AddHostedService<DaemonService>();
                    
                    services.AddMassTransit(x =>
                    {
                        x.UsingRabbitMq((context, cfg) =>
                        {
                            cfg.Host("rabbitmq", "/", h =>
                            {
                                h.Username(Environment.GetEnvironmentVariable("RABBITMQ_DEFAULT_USER"));
                                h.Password(Environment.GetEnvironmentVariable("RABBITMQ_DEFAULT_PASS"));
                            });
                        });
                    });
                    services.AddMassTransitHostedService();

                    services.AddEntityFrameworkNpgsql().AddDbContext<DatabaseContext>();
                });
    }
}