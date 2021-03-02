using EntityFramework.Exceptions.PostgreSQL;
using Microsoft.EntityFrameworkCore;
using OneGate.Backend.Core.Assets.Database.Models;

namespace OneGate.Backend.Core.Assets.Database
{
    public sealed class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        { }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseExceptionProcessor();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Asset>()
                .HasIndex(x => new {x.Type, x.ExchangeId, x.Ticker})
                .IsUnique();

            modelBuilder.Entity<Exchange>()
                .HasIndex(x => new {x.Title})
                .IsUnique();

            modelBuilder.Entity<Asset>()
                .HasDiscriminator(x => x.Type)
                .HasValue<StockAsset>("STOCK")
                .HasValue<IndexAsset>("INDEX");
        }

        public DbSet<Exchange> Exchanges { get; set; }
        public DbSet<Asset> Assets { get; set; }
    }
}