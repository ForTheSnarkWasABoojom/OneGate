using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OneGate.Backend.Core.Base.Database;
using OneGate.Backend.Core.Base.Logging;
using OneGate.Backend.Core.Timeseries.Consumers;
using OneGate.Backend.Core.Timeseries.Database;
using OneGate.Backend.Core.Timeseries.Database.Repository;
using OneGate.Backend.Transport.Bus;
using OneGate.Backend.Transport.Bus.Options;

namespace OneGate.Backend.Core.Timeseries
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
                    var dbConfiguration = hostContext.Configuration.GetSection(DatabaseConnectionOptionsSection);
                    var connectionString = ConnectionString.Build(dbConfiguration.Get<DatabaseConnectionOptions>());
                    services.AddEntityFrameworkNpgsql()
                        .AddDbContext<DatabaseContext>(p => p.UseNpgsql(connectionString));

                    // Services.
                    services.AddTransient<IService, Service>();
                    
                    // Repositories.
                    services.AddTransient<IOhlcSeriesRepository, OhlcSeriesRepository>();
                    services.AddTransient<IPointSeriesRepository, PointSeriesRepository>();

                    // Mass Transit.
                    var rabbitMqSection = configuration.GetSection(RabbitMqOptionsSection);
                    services.UseMassTransit(rabbitMqSection.Get<RabbitMqOptions>(), new[]
                    {
                        new KeyValuePair<Type, Type>(typeof(Consumer), typeof(ConsumerSettings)),
                    });
                }).UseBaseLogging();
    }
}