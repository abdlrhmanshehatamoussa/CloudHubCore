using CloudHub.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CloudHub.ServiceProvider.Data
{

    internal class MyContext : DbContext
    {
        public MyContext()
        {

        }
        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {
            int migrationsCount = Database.GetAppliedMigrations().Count();
            if (migrationsCount == 0) Database.Migrate();
        }

        public virtual DbSet<Client> Clients { get; set; } = null!;
        public virtual DbSet<Feature> Features { get; set; } = null!;
        public virtual DbSet<Login> Logins { get; set; } = null!;
        public virtual DbSet<LoginType> LoginTypes { get; set; } = null!;
        public virtual DbSet<Nonce> Nonces { get; set; } = null!;
        public virtual DbSet<PaymentGateway> PaymentGateways { get; set; } = null!;
        public virtual DbSet<Purchase> Purchases { get; set; } = null!;
        public virtual DbSet<Event> Events { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UserToken> UserTokens { get; set; } = null!;
        public virtual DbSet<Tenant> Tenants { get; set; } = null!;
        public virtual DbSet<PublicCollection> PublicCollections { get; set; } = null!;
        public virtual DbSet<PublicDocument> PublicDocuments { get; set; } = null!;
        public virtual DbSet<PrivateCollection> PrivateCollections { get; set; } = null!;
        public virtual DbSet<PrivateDocument> PrivateDocuments { get; set; } = null!;


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured == false)
            {
                optionsBuilder.UseNpgsql("Host=127.0.0.1;Database=cloudhub-api-core-local;Username=postgres;Password=123456");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            DataSeeder.Seed(modelBuilder);
            EntityMapper.MapEntities(modelBuilder);
        }
    }
}
