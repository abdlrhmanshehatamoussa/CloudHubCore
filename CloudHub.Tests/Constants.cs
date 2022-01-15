using CloudHub.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace CloudHub.Tests
{
    internal class Constants
    {
        public const string PSQL_HOST = "Host=127.0.0.1;Database=cloudhub-api-core-local;Username=postgres;Password=123456";
        public static UnitOfWork UnitOfWork
        {
            get
            {
                DbContextOptionsBuilder builder = new ();
                builder.UseNpgsql(PSQL_HOST);
                UnitOfWork uow = new(new PostgreContext(builder.Options));
                return uow;
            }
        }
    }
}
