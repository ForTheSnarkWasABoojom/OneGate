using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OneGate.Backend.Core.Assets.Consumers;
using OneGate.Backend.Core.Assets.Mapping;
using OneGate.Backend.Core.Assets.Services;
using OneGate.Backend.Core.Base.Database;
using OneGate.Backend.Core.Base.Logging;
using OneGate.Backend.Core.Assets.Database;
using OneGate.Backend.Core.Assets.Database.Repository;
using OneGate.Backend.Core.Base.Exceptions;
using OneGate.Backend.Transport.Bus;
using OneGate.Backend.Transport.Bus.Options;

namespace OneGate.Backend.Core.Assets
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

                    // Services.
                    services.AddTransient<IAssetService, AssetService>();
                    services.AddTransient<IExchangeService, ExchangeService>();
                    services.AddTransient<ILayerService, LayerService>();

                    // Repositories.
                    services.AddTransient<IExchangeRepository, ExchangeRepository>();
                    services.AddTransient<IAssetRepository, AssetRepository>();
                    services.AddTransient<ILayerRepository, LayerRepository>();
                    
                    // Automapper.
                    services.AddAutoMapper(p =>
                        p.AddProfile<MappingProfile>()
                    );

                    // Mass Transit.
                    var rabbitMqSection = configuration.GetSection(RabbitMqOptionsSection);
                    services.UseTransportBus(rabbitMqSection.Get<RabbitMqOptions>(), new[]
                    {
                        new KeyValuePair<Type, Type>(typeof(RpcWorker), typeof(RpcWorkerSettings)),
                    });

                    services.AddSingleton<IResponseExceptionHandler, ResponseExceptionHandler>();
                }).UseBaseLogging();
    }
}