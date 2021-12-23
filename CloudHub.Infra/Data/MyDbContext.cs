using CloudHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CloudHub.Infra.Data
{
    public partial class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<UserAction> Actions { get; set; } = null!;
        public virtual DbSet<Application> Applications { get; set; } = null!;
        public virtual DbSet<ApplicationSecret> ApplicationSecrets { get; set; } = null!;
        public virtual DbSet<Client> Clients { get; set; } = null!;
        public virtual DbSet<ClientType> ClientTypes { get; set; } = null!;
        public virtual DbSet<ClientApplicationRelation> ClientApplicationRelations { get; set; } = null!;
        public virtual DbSet<Feature> Features { get; set; } = null!;
        public virtual DbSet<Login> Logins { get; set; } = null!;
        public virtual DbSet<LoginType> LoginTypes { get; set; } = null!;
        public virtual DbSet<Nonce> Nonces { get; set; } = null!;
        public virtual DbSet<PaymentGateway> PaymentGateways { get; set; } = null!;
        public virtual DbSet<Purchase> Purchases { get; set; } = null!;
        public virtual DbSet<Release> Releases { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UserToken> UserTokens { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserAction>(entity =>
            {
                entity.ToTable("actions");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AppVersion).HasColumnName("app_version");

                entity.Property(e => e.ApplicationId).HasColumnName("application_id");

                entity.Property(e => e.Category)
                    .HasMaxLength(255)
                    .HasColumnName("category");

                entity.Property(e => e.CreatedOn)
                    .HasColumnName("created_on")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .HasColumnName("description");

                entity.Property(e => e.Payload)
                    .HasMaxLength(255)
                    .HasColumnName("payload");

                entity.HasOne(d => d.Application)
                    .WithMany(p => p.Actions)
                    .HasForeignKey(d => d.ApplicationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("actions_application_id_foreign");
            });

            modelBuilder.Entity<Application>(entity =>
            {
                entity.ToTable("applications");

                entity.HasIndex(e => e.Guid, "applications_guid_unique")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasColumnName("active")
                    .HasDefaultValueSql("true");

                entity.Property(e => e.CreatedOn)
                    .HasColumnName("created_on")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.Guid).HasColumnName("guid");

                entity.Property(e => e.ModifiedOn)
                    .HasColumnName("modified_on")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<ApplicationSecret>(entity =>
            {
                entity.ToTable("application_secrets");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasColumnName("active")
                    .HasDefaultValueSql("true");

                entity.Property(e => e.ApplicationId).HasColumnName("application_id");

                entity.Property(e => e.CreatedOn)
                    .HasColumnName("created_on")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.ModifiedOn)
                    .HasColumnName("modified_on")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.SecretKey).HasColumnName("secret_key");

                entity.Property(e => e.SecretValue).HasColumnName("secret_value");

                entity.HasOne(d => d.Application)
                    .WithMany(p => p.ApplicationSecrets)
                    .HasForeignKey(d => d.ApplicationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("application_secrets_application_id_foreign");
            });

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

            modelBuilder.Entity<ClientApplicationRelation>(entity =>
            {
                entity.ToTable("clients_applications");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasColumnName("active")
                    .HasDefaultValueSql("true");

                entity.Property(e => e.ApplicationId).HasColumnName("application_id");

                entity.Property(e => e.ClientId).HasColumnName("client_id");

                entity.HasOne(d => d.Application)
                    .WithMany(p => p.ClientsApplications)
                    .HasForeignKey(d => d.ApplicationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("clients_applications_application_id_foreign");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.ClientsApplications)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("clients_applications_client_id_foreign");
            });

            modelBuilder.Entity<Feature>(entity =>
            {
                entity.ToTable("features");

                entity.HasIndex(e => new { e.ApplicationId, e.GlobalId }, "features_global_id_unique")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasColumnName("active")
                    .HasDefaultValueSql("true");

                entity.Property(e => e.ApplicationId).HasColumnName("application_id");

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

                entity.HasOne(d => d.Application)
                    .WithMany(p => p.Features)
                    .HasForeignKey(d => d.ApplicationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("features_app_id_foreign");
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

                entity.Property(e => e.ApplicationId).HasColumnName("application_id");

                entity.Property(e => e.ClientId).HasColumnName("client_id");

                entity.Property(e => e.ConsumedOn).HasColumnName("consumed_on");

                entity.Property(e => e.CreatedOn)
                    .HasColumnName("created_on")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.Token)
                    .HasMaxLength(255)
                    .HasColumnName("token");

                entity.HasOne(d => d.Application)
                    .WithMany(p => p.Nonces)
                    .HasForeignKey(d => d.ApplicationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("nonces_application_id_foreign");

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

            modelBuilder.Entity<Release>(entity =>
            {
                entity.ToTable("releases");

                entity.HasIndex(e => new { e.ApplicationId, e.ReleaseName }, "releases_name_unique")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ApplicationId).HasColumnName("application_id");

                entity.Property(e => e.Notes)
                    .HasMaxLength(255)
                    .HasColumnName("notes");

                entity.Property(e => e.ReleaseDate)
                    .HasColumnName("release_date")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.ReleaseName)
                    .HasMaxLength(255)
                    .HasColumnName("release_name");

                entity.HasOne(d => d.Application)
                    .WithMany(p => p.Releases)
                    .HasForeignKey(d => d.ApplicationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("releases_app_id_foreign");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");

                entity.HasIndex(e => new { e.Email, e.ApplicationId }, "users_email_unique")
                    .IsUnique();

                entity.HasIndex(e => e.GlobalId, "users_global_id_unique")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasColumnName("active")
                    .HasDefaultValueSql("true");

                entity.Property(e => e.ApplicationId).HasColumnName("application_id");

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

                entity.HasOne(d => d.Application)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.ApplicationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("users_application_id_foreign");
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
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
