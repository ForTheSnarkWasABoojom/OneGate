using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OneGate.Backend.Core.Base;
using OneGate.Backend.Core.Base.Database;
using OneGate.Backend.Core.Base.Logging;
using OneGate.Backend.Core.Users.Consumers;
using OneGate.Backend.Core.Users.Database;
using OneGate.Backend.Core.Users.Database.Models;
using OneGate.Backend.Core.Users.Database.Repository;
using OneGate.Backend.Core.Users.Mapping;
using OneGate.Backend.Core.Users.Services;
using OneGate.Backend.Transport.Bus;
using OneGate.Backend.Transport.Bus.Options;

namespace OneGate.Backend.Core.Users
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
                    services.AddTransient<IAccountService, AccountService>();
                    services.AddTransient<IOrderService, OrderService>();
                    services.AddTransient<IPortfolioService, PortfolioService>();

                    // Repositories.
                    services.AddTransient<IAccountRepository, AccountRepository>();
                    services.AddTransient<IOrderRepository, OrderRepository>();
                    services.AddTransient<IPortfolioRepository, PortfolioRepository>();

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