using CloudHub.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CloudHub.Infra.Data
{
    public partial class PostgreContext : DbContext
    {
        public PostgreContext(DbContextOptions options) : base(options) { }

        public virtual DbSet<Client> Clients { get; set; } = null!;
        public virtual DbSet<Feature> Features { get; set; } = null!;
        public virtual DbSet<Login> Logins { get; set; } = null!;
        public virtual DbSet<LoginType> LoginTypes { get; set; } = null!;
        public virtual DbSet<Nonce> Nonces { get; set; } = null!;
        public virtual DbSet<PaymentGateway> PaymentGateways { get; set; } = null!;
        public virtual DbSet<Purchase> Purchases { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UserToken> UserTokens { get; set; } = null!;
        public virtual DbSet<Tenant> Tenants { get; set; } = null!;
        public virtual DbSet<PublicCollection> PublicCollections { get; set; } = null!;
        public virtual DbSet<PublicDocument> PublicDocuments { get; set; } = null!;
        public virtual DbSet<PrivateCollection> PrivateCollections { get; set; } = null!;
        public virtual DbSet<PrivateDocument> PrivateDocuments { get; set; } = null!;


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            DataSeeder.Seed(modelBuilder);
            EntityMapper.MapEntities(modelBuilder);
        }
    }
}
