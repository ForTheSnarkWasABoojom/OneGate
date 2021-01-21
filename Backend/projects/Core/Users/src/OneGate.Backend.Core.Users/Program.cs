using System;
using System.Collections.Generic;
using System.Threading;
using AutoMapper;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OneGate.Backend.Core.Users.Database;
using OneGate.Backend.Core.Users.Database.Models;
using OneGate.Backend.Core.Users.Database.Repository;
using OneGate.Backend.Transport.Bus;
using OneGate.Backend.Transport.Bus.Options;

namespace OneGate.Backend.Core.Users
{
    public class Program
    {
        private const string RabbitMqOptionsSection = "RabbitMq";

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
                    
                    services.AddTransient<IAccountRepository, AccountRepository>();
                    services.AddTransient<IOrderRepository, OrderRepository>();
                    services.AddTransient<IPortfolioRepository, PortfolioRepository>();

                    services.AddAutoMapper(typeof(MappingProfile));
                    
                    // Migration.
                    Migrate(services);

                    // Mass Transit.
                    var configuration = hostContext.Configuration.GetSection("OneGate");
                    var rabbitMqSection = configuration.GetSection(RabbitMqOptionsSection);
                    services.UseMassTransit(rabbitMqSection.Get<RabbitMqOptions>(), new[]
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

            var account = new Account
            {
                FirstName = "ONEGATE",
                LastName = "ADMINISTRATOR",
                Email = Environment.GetEnvironmentVariable("API_ADMIN_EMAIL"),
                Password = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                    password: Environment.GetEnvironmentVariable("API_ADMIN_PASSWORD"),
                    salt: new byte[128 / 8],
                    prf: KeyDerivationPrf.HMACSHA1,
                    iterationCount: 10000,
                    numBytesRequested: 256 / 8)),
                IsAdmin = true
            };
            
            db.Accounts.Add(account);
            db.SaveChanges();
        }
    }
}