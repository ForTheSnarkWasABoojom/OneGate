using System;
using EntityFramework.Exceptions.PostgreSQL;
using Microsoft.EntityFrameworkCore;
using OneGate.Backend.Core.Users.Database.Models;

namespace OneGate.Backend.Core.Users.Database
{
    public sealed class DatabaseContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseExceptionProcessor();

            optionsBuilder.UseNpgsql(
                $"Host=user_db;Port=5432;" +
                $"Database={Environment.GetEnvironmentVariable("POSTGRES_DB")};" +
                $"Username={Environment.GetEnvironmentVariable("POSTGRES_USER")};" +
                $"Password={Environment.GetEnvironmentVariable("POSTGRES_PASSWORD")}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .HasIndex(x => new {x.Email})
                .IsUnique();

            modelBuilder.Entity<Order>()
                .HasDiscriminator(x => x.Type)
                .HasValue<MarketOrder>("MARKET")
                .HasValue<LimitOrder>("LIMIT")
                .HasValue<StopOrder>("STOP");
        }

        public DbSet<Account> Accounts { get; set; }

        public DbSet<Order> Orders { get; set; }
        public DbSet<MarketOrder> MarketOrders { get; set; }
        public DbSet<StopOrder> StopOrders { get; set; }
        public DbSet<LimitOrder> LimitOrders { get; set; }

        public DbSet<Portfolio> Portfolios { get; set; }
        public DbSet<PortfolioAssetLink> PortfolioAssetLinks { get; set; }
    }
}