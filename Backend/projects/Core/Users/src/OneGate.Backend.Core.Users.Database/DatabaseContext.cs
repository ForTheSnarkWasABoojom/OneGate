using EntityFramework.Exceptions.PostgreSQL;
using Microsoft.EntityFrameworkCore;
using OneGate.Backend.Core.Users.Database.Models;

namespace OneGate.Backend.Core.Users.Database
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
        public DbSet<Administrator> Administrators { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Portfolio> Portfolios { get; set; }
    }
}