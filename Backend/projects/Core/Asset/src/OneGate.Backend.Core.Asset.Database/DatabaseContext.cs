using System;
using EntityFramework.Exceptions.PostgreSQL;
using Microsoft.EntityFrameworkCore;
using OneGate.Backend.Core.Asset.Database.Models;

namespace OneGate.Backend.Core.Asset.Database
{
    public sealed class DatabaseContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseExceptionProcessor();

            optionsBuilder.UseNpgsql(
                $"Host=asset_db;Port=5432;" +
                $"Database={Environment.GetEnvironmentVariable("POSTGRES_DB")};" +
                $"Username={Environment.GetEnvironmentVariable("POSTGRES_USER")};" +
                $"Password={Environment.GetEnvironmentVariable("POSTGRES_PASSWORD")}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AssetBase>()
                .HasIndex(x => new {x.Type, x.ExchangeId, x.Ticker})
                .IsUnique();
            
            modelBuilder.Entity<Layout>()
                .HasIndex(x => new {x.Name})
                .IsUnique();

            modelBuilder.Entity<Exchange>()
                .HasIndex(x => new {x.Title})
                .IsUnique();

            modelBuilder.Entity<AssetBase>()
                .HasDiscriminator(x => x.Type)
                .HasValue<StockAsset>("STOCK")
                .HasValue<IndexAsset>("INDEX");
        }

        public DbSet<Exchange> Exchanges { get; set; }
        public DbSet<Layout> Layouts { get; set; }
        public DbSet<AssetBase> Assets { get; set; }
        public DbSet<StockAsset> StocksAssets { get; set; }
        public DbSet<IndexAsset> IndexAssets { get; set; }
    }
}