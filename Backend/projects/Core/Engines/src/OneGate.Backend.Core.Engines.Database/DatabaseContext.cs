using EntityFramework.Exceptions.PostgreSQL;
using Microsoft.EntityFrameworkCore;
using OneGate.Backend.Core.Engines.Database.Models;

namespace OneGate.Backend.Core.Engines.Database
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
            modelBuilder.Entity<AssetMapping>()
                .HasIndex(x => new { x.AssetId, x.EngineId })
                .IsUnique();
        }
        
        public DbSet<AssetMapping> AssetMappings { get; set; }
    }
}