using EntityFramework.Exceptions.PostgreSQL;
using Microsoft.EntityFrameworkCore;
using OneGate.Backend.Core.Timeseries.Database.Models;

namespace OneGate.Backend.Core.Timeseries.Database
{
    public sealed class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseExceptionProcessor();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Layer>()
                .HasIndex(x => new
                {
                    x.OwnerId,
                    x.AssetId,
                    x.Interval
                })
                .IsUnique();

            modelBuilder.Entity<Artifact>()
                .HasIndex(x => new
                {
                    x.Type,
                    x.LayerId,
                    x.Timestamp
                })
                .IsUnique();
            
            modelBuilder.Entity<Ohlc>()
                .HasIndex(x => new
                {
                    x.AssetId,
                    x.Interval,
                    x.Timestamp
                })
                .IsUnique();

            modelBuilder.Entity<Artifact>()
                .HasDiscriminator(x => x.Type)
                .HasValue<PointArtifact>("POINT")
                .HasValue<AdviceArtifact>("ADVICE");
        }
        
        public DbSet<Ohlc> Ohlcs { get; set; }
        public DbSet<Layer> Layers { get; set; }
        public DbSet<Artifact> Artifacts { get; set; }
    }
}