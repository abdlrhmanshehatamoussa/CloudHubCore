using CloudHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CloudHub.Infra.Data
{
    public partial class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
            Database.Migrate();
        }

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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>(entity =>
            {
                entity.ToTable("clients");

                entity.HasIndex(e => e.ClientKey, "clients_client_key_unique")
                    .IsUnique();

                entity.HasIndex(e => e.ClientSecret, "clients_client_secret_unique")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasColumnName("active")
                    .HasDefaultValueSql("true");

                entity.Property(e => e.ClientKey)
                    .HasMaxLength(255)
                    .HasColumnName("client_key");

                entity.Property(e => e.ClientSecret)
                    .HasMaxLength(255)
                    .HasColumnName("client_secret");

                entity.Property(e => e.ClientTypeId).HasColumnName("client_type_id");

                entity.Property(e => e.CreatedOn)
                    .HasColumnName("created_on")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.ModifiedOn)
                    .HasColumnName("modified_on")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");

                entity.HasOne(d => d.ClientType)
                    .WithMany(p => p.Clients)
                    .HasForeignKey(d => d.ClientTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("clients_client_type_id_foreign");
            });

            modelBuilder.Entity<ClientType>(entity =>
            {
                entity.ToTable("client_types");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasColumnName("active")
                    .HasDefaultValueSql("true");

                entity.Property(e => e.CreatedOn)
                    .HasColumnName("created_on")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.ModifiedOn)
                    .HasColumnName("modified_on")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Feature>(entity =>
            {
                entity.ToTable("features");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasColumnName("active")
                    .HasDefaultValueSql("true");

                entity.Property(e => e.CreatedOn)
                    .HasColumnName("created_on")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .HasColumnName("description");

                entity.Property(e => e.GlobalId).HasColumnName("global_id");

                entity.Property(e => e.ModifiedOn)
                    .HasColumnName("modified_on")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Login>(entity =>
            {
                entity.ToTable("logins");

                entity.HasIndex(e => e.UserId, "logins_user_id_unique")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.LoginTypeId).HasColumnName("login_type_id");

                entity.Property(e => e.Passcode)
                    .HasMaxLength(255)
                    .HasColumnName("passcode");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.LoginType)
                    .WithMany(p => p.Logins)
                    .HasForeignKey(d => d.LoginTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("logins_login_type_id_foreign");

                entity.HasOne(d => d.User)
                    .WithOne(p => p.Login)
                    .HasForeignKey<Login>(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("logins_user_id_foreign");
            });

            modelBuilder.Entity<LoginType>(entity =>
            {
                entity.ToTable("login_types");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasColumnName("active")
                    .HasDefaultValueSql("true");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Nonce>(entity =>
            {
                entity.ToTable("nonces");

                entity.HasIndex(e => e.Token, "nonces_tokens_unique")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ClientId).HasColumnName("client_id");

                entity.Property(e => e.ConsumedOn).HasColumnName("consumed_on");

                entity.Property(e => e.CreatedOn)
                    .HasColumnName("created_on")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.Token)
                    .HasMaxLength(255)
                    .HasColumnName("token");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.Nonces)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("nonces_client_id_foreign");
            });

            modelBuilder.Entity<PaymentGateway>(entity =>
            {
                entity.ToTable("payment_gateways");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasColumnName("active")
                    .HasDefaultValueSql("true");

                entity.Property(e => e.CreatedOn)
                    .HasColumnName("created_on")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.ModifiedOn)
                    .HasColumnName("modified_on")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Purchase>(entity =>
            {
                entity.ToTable("purchases");

                entity.HasIndex(e => new { e.FeatureId, e.UserId }, "purchases_user_id_feature_id_unique")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedOn)
                    .HasColumnName("created_on")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.FeatureId).HasColumnName("feature_id");

                entity.Property(e => e.Payload).HasColumnName("payload");

                entity.Property(e => e.PaymentGatewayId).HasColumnName("payment_gateway_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.Validation).HasColumnName("validation");

                entity.HasOne(d => d.Feature)
                    .WithMany(p => p.Purchases)
                    .HasForeignKey(d => d.FeatureId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("purchases_feature_id_foreign");

                entity.HasOne(d => d.PaymentGateway)
                    .WithMany(p => p.Purchases)
                    .HasForeignKey(d => d.PaymentGatewayId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("purchases_payment_gateway_id_foreign");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Purchases)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("purchases_user_id_foreign");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");

                entity.HasIndex(e => e.GlobalId, "users_global_id_unique")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasColumnName("active")
                    .HasDefaultValueSql("true");

                entity.Property(e => e.CreatedOn)
                    .HasColumnName("created_on")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .HasColumnName("email");

                entity.Property(e => e.GlobalId)
                    .HasMaxLength(255)
                    .HasColumnName("global_id");

                entity.Property(e => e.ImageUrl)
                    .HasMaxLength(255)
                    .HasColumnName("image_url");

                entity.Property(e => e.ModifiedOn)
                    .HasColumnName("modified_on")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<UserToken>(entity =>
            {
                entity.ToTable("user_tokens");

                entity.HasIndex(e => e.Token, "user_tokens_token_unique")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasColumnName("active")
                    .HasDefaultValueSql("true");

                entity.Property(e => e.CreatedOn)
                    .HasColumnName("created_on")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.ExpiryDate)
                    .HasColumnName("expiry_date")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.Token)
                    .HasMaxLength(255)
                    .HasColumnName("token");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserTokens)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("user_tokens_user_id_foreign");
            });

            OnModelCreatingPartial(modelBuilder);
            Seed(modelBuilder);
        }

        private void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ClientType>().HasData(
                new ClientType() { Active = true, Name = "Admin", Id = ClientTypeValues.ADMIN },
                new ClientType() { Active = true, Name = "Application", Id = ClientTypeValues.APP },
                new ClientType() { Active = true, Name = "Dashboard", Id = ClientTypeValues.DASHBOARD }
            );
            modelBuilder.Entity<LoginType>().HasData(
                new LoginType() { Active = true, Name = "Google", Id = LoginTypeValues.LOGIN_TYPE_GOOGLE },
                new LoginType() { Active = true, Name = "Basic", Id = LoginTypeValues.LOGIN_TYPE_BASIC },
                new LoginType() { Active = true, Name = "Facebook", Id = LoginTypeValues.LOGIN_TYPE_FACEBOOK },
                new LoginType() { Active = true, Name = "Linked In", Id = LoginTypeValues.LOGIN_TYPE_LINKEDIN }
            );
            modelBuilder.Entity<PaymentGateway>().HasData(
                new PaymentGateway() { Active = true, Name = "Google Play Billing", Id = PaymentGatewayValues.GOOGLE_PLAY_BILLING },
                new PaymentGateway() { Active = true, Name = "Paypal", Id = PaymentGatewayValues.PAYPAL }
            );
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
