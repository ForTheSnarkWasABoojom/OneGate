using System;
using EntityFramework.Exceptions.PostgreSQL;
using Microsoft.EntityFrameworkCore;
using OneGate.Backend.Database.Models;

namespace OneGate.Backend.Database
{
    public sealed class DatabaseContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseExceptionProcessor();

            optionsBuilder.UseNpgsql(
                $"Host=postgres;Port=5432;" +
                $"Database={Environment.GetEnvironmentVariable("POSTGRES_DB")};" +
                $"Username={Environment.GetEnvironmentVariable("POSTGRES_USER")};" +
                $"Password={Environment.GetEnvironmentVariable("POSTGRES_PASSWORD")}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .HasIndex(x => new {x.Email})
                .IsUnique();

            modelBuilder.Entity<AssetBase>()
                .HasIndex(x => new {x.Type, x.ExchangeId, x.Ticker})
                .IsUnique();

            modelBuilder.Entity<Exchange>()
                .HasIndex(x => new {x.Title})
                .IsUnique();

            modelBuilder.Entity<OhlcSeries>()
                .HasIndex(x => new {x.AssetId, x.Interval, x.Timestamp})
                .IsUnique();

            modelBuilder.Entity<PointSeries>()
                .HasIndex(x => new {x.AssetId, x.LayoutId, x.Timestamp})
                .IsUnique();

            modelBuilder.Entity<AssetBase>()
                .HasDiscriminator(x => x.Type)
                .HasValue<StockAsset>("STOCK")
                .HasValue<IndexAsset>("INDEX");

            modelBuilder.Entity<OrderBase>()
                .HasDiscriminator(x => x.Type)
                .HasValue<MarketOrder>("MARKET")
                .HasValue<LimitOrder>("LIMIT")
                .HasValue<StopOrder>("STOP");
        }

        public DbSet<Account> Accounts { get; set; }

        public DbSet<Exchange> Exchanges { get; set; }

        public DbSet<OhlcSeries> OhlcSeries { get; set; }
        public DbSet<PointSeries> PointSeries { get; set; }
        public DbSet<Layout> Layouts { get; set; }
        public DbSet<AssetBase> Assets { get; set; }
        public DbSet<StockAsset> StocksAssets { get; set; }
        public DbSet<IndexAsset> IndexAssets { get; set; }

        public DbSet<OrderBase> Orders { get; set; }
        public DbSet<MarketOrder> MarketOrders { get; set; }
        public DbSet<StopOrder> StopOrders { get; set; }
        public DbSet<LimitOrder> LimitOrders { get; set; }

        public DbSet<Portfolio> Portfolios { get; set; }
        public DbSet<PortfolioAssetLink> PortfolioAssetLinks { get; set; }
    }
}