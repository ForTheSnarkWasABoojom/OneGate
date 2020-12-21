using System;
using EntityFramework.Exceptions.PostgreSQL;
using Microsoft.EntityFrameworkCore;
using OneGate.Backend.Core.Timeseries.Database.Models;

namespace OneGate.Backend.Core.Timeseries.Database
{
    public sealed class DatabaseContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseExceptionProcessor();

            optionsBuilder.UseNpgsql(
                $"Host=series_db;Port=5432;" +
                $"Database={Environment.GetEnvironmentVariable("POSTGRES_DB")};" +
                $"Username={Environment.GetEnvironmentVariable("POSTGRES_USER")};" +
                $"Password={Environment.GetEnvironmentVariable("POSTGRES_PASSWORD")}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OhlcSeries>()
                .HasIndex(x => new {x.AssetId, x.Interval, x.Timestamp})
                .IsUnique();

            modelBuilder.Entity<PointSeries>()
                .HasIndex(x => new {x.AssetId, x.LayoutId, x.Timestamp})
                .IsUnique();
        }
        public DbSet<OhlcSeries> OhlcSeries { get; set; }
        public DbSet<PointSeries> PointSeries { get; set; }
    }
}