using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore.Migrations.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OneGate.Backend.Core.User.Database;
using OneGate.Backend.Core.User.Database.Models;
using OneGate.Backend.Core.User.Service.Repository;
using OneGate.Backend.Transport.Bus;

namespace OneGate.Backend.Core.User.Service
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

                    services.AddTransient<IUserService, UserService>();
                    
                    services.AddTransient<IAccountRepository, AccountRepository>();
                    services.AddTransient<IOrderRepository, OrderRepository>();
                    services.AddTransient<IPortfolioRepository, PortfolioRepository>();
                    services.AddTransient<IPorfolioAssetLinkRepository, PorfolioAssetLinkRepository>();
                    
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