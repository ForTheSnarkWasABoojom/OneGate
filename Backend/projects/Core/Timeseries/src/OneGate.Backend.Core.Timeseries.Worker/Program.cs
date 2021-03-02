using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OneGate.Backend.Core.Shared.Database;
using OneGate.Backend.Core.Shared.Logging;
using OneGate.Backend.Core.Timeseries.Database;
using OneGate.Backend.Core.Timeseries.Worker.Consumers;
using OneGate.Backend.Core.Timeseries.Worker.Services;
using OneGate.Backend.Transport.Bus;
using OneGate.Backend.Transport.Bus.Options;

namespace OneGate.Backend.Core.Timeseries.Worker
{
    public class Program
    {
        private const string RabbitMqOptionsSection = "RabbitMq";
        private const string DatabaseConnectionOptionsSection = "DatabaseConnection";

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
                    
                    // Database.
                    var dbConfiguration = configuration.GetSection(DatabaseConnectionOptionsSection);
                    var dbOptions = dbConfiguration.Get<DatabaseConnectionOptions>();
                    
                    var connectionString = ConnectionString.Build(dbOptions);
                    services.AddDbContext<DatabaseContext>(p => p.UseNpgsql(connectionString));
                    
                    // Transport.
                    var rabbitMqSection = configuration.GetSection(RabbitMqOptionsSection);
                    services.UseTransportBus(rabbitMqSection.Get<RabbitMqOptions>(), new []
                    {
                        new KeyValuePair<Type, Type>(typeof(WorkerConsumer), typeof(WorkerConsumerSettings))
                    });
                    
                    // Services.
                    services.AddTransient<ISeriesService, SeriesService>();
                }).UseLogging();
    }
}