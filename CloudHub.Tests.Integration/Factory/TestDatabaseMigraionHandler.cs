using CloudHub.Infra.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CloudHub.Tests.Integration
{
    class TestDatabaseMigraionHandler
    {
        public TestDatabaseMigraionHandler(PostgreContext context) => _context = context;

        private readonly PostgreContext _context;

        public void HandleMigrations()
        {
            _context.Database.EnsureDeleted();
            int migrationsCount = _context.Database.GetAppliedMigrations().Count();
            if (migrationsCount == 0)
            {
                _context.Database.Migrate();
            }
        }
    }

}
