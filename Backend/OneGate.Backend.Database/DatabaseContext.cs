using System;
using Microsoft.EntityFrameworkCore;
using OneGate.Backend.Database.Models;
using Index = OneGate.Backend.Database.Models.Index;

namespace OneGate.Backend.Database
{
    public sealed class DatabaseContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(
                $"Host=postgres;Port=5432;" +
                $"Database={Environment.GetEnvironmentVariable("POSTGRES_DB")};" +
                $"Username={Environment.GetEnvironmentVariable("POSTGRES_USER")};" +
                $"Password={Environment.GetEnvironmentVariable("POSTGRES_PASSWORD")}");
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AssetBase>()
                .HasDiscriminator(x => x.Type)
                .HasValue<Stock>("STOCK")
                .HasValue<Index>("INDEX");
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Exchange> Exchanges { get; set; }
        public DbSet<Ohlc> Ohlcs { get; set; }
        public DbSet<AssetBase> Assets { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Index> Indexes { get; set; }
    }
}