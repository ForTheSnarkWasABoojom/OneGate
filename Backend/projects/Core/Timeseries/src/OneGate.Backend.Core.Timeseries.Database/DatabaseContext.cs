using EntityFramework.Exceptions.PostgreSQL;
using Microsoft.EntityFrameworkCore;
using OneGate.Backend.Core.Timeseries.Database.Models;

namespace OneGate.Backend.Core.Timeseries.Database
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
            modelBuilder.Entity<Series>()
                .HasIndex(x => new { LayoutId = x.LayerId, x.Timestamp})
                .IsUnique();
            
            modelBuilder.Entity<Series>()
                .HasDiscriminator(x => x.Type)
                .HasValue<OhlcSeries>("OHLC")
                .HasValue<PointSeries>("POINT");
        }
        
        public DbSet<Series> Series { get; set; }
    }
}