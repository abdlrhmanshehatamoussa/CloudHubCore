using CloudHub.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace CloudHub.API.Startup
{
    public static class Migrations
    {
        public static void ApplyPendingMigrations(string connectionString)
        {
            DbContextOptionsBuilder dbBuilder = new();
            dbBuilder.UseNpgsql(connectionString);
            PostgreContext context = new(dbBuilder.Options);
            if (context.Database.GetPendingMigrations().ToList().Count > 0) { context.Database.Migrate(); }
        }
    }
}
