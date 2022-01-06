using CloudHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CloudHub.Infra.Data
{
    public partial class PostgreDatabase : DbContext
    {
        public PostgreDatabase(DbContextOptions<PostgreDatabase> options) : base(options) { }

        public virtual DbSet<Admin> Admins { get; set; } = null!;
        public virtual DbSet<AdminType> AdminTypes { get; set; } = null!;
        public virtual DbSet<Client> Clients { get; set; } = null!;
        public virtual DbSet<ClientType> ClientTypes { get; set; } = null!;
        public virtual DbSet<Feature> Features { get; set; } = null!;
        public virtual DbSet<Login> Logins { get; set; } = null!;
        public virtual DbSet<LoginType> LoginTypes { get; set; } = null!;
        public virtual DbSet<Nonce> Nonces { get; set; } = null!;
        public virtual DbSet<PaymentGateway> PaymentGateways { get; set; } = null!;
        public virtual DbSet<Purchase> Purchases { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UserToken> UserTokens { get; set; } = null!;
        public virtual DbSet<Collection> Collections { get; set; } = null!;

        private readonly List<IBaseMapper> ModelBuilders = new()
        {
            new ClientMapper(),
            new ClientTypeMapper(),
            new FeatureMapper(),
            new LoginMapper(),
            new LoginTypeMapper(),
            new NonceMapper(),
            new PaymentGatewayMapper(),
            new PurchaseMapper(),
            new UserMapper(),
            new UserTokenMapper(),
            new CollectionsMapper(),
            new AdminMapper(),
            new AdminTypesMapper(),
        };

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var builder in ModelBuilders)
            {
                builder.Map(modelBuilder);
            }

            OnModelCreatingPartial(modelBuilder);

            DataSeeder.Seed(modelBuilder);
        }
    }
}
