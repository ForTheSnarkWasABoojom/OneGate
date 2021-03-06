using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace OneGate.Backend.Core.Assets.Database.Migrations
{
    public class DatabaseContextFactory : IDesignTimeDbContextFactory<DatabaseContext>
    {
        public DatabaseContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();
            optionsBuilder.UseNpgsql(p => p.MigrationsAssembly("OneGate.Backend.Core.Assets.Database.Migrations"));

            return new DatabaseContext(optionsBuilder.Options);
        }
    }
}