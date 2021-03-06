// <auto-generated />
using System;
using System.Text.Json;
using CloudHub.ServiceProvider.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CloudHub.ServiceProvider.Data
{
    [DbContext(typeof(MyContext))]
    partial class PostgreContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CloudHub.Domain.Models.Client", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool>("Active")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasColumnName("active")
                        .HasDefaultValueSql("true");

                    b.Property<string>("ClientKey")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("client_key");

                    b.Property<string>("ClientSecret")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("client_secret");

                    b.Property<DateTime>("CreatedOn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_on")
                        .HasDefaultValueSql("now()");

                    b.Property<DateTime>("ModifiedOn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("modified_on")
                        .HasDefaultValueSql("now()");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("name");

                    b.Property<int>("TenantId")
                        .HasColumnType("integer")
                        .HasColumnName("tenant_id");

                    b.HasKey("Id");

                    b.HasIndex("TenantId");

                    b.HasIndex(new[] { "ClientKey" }, "clients_client_key_unique")
                        .IsUnique();

                    b.HasIndex(new[] { "ClientSecret" }, "clients_client_secret_unique")
                        .IsUnique();

                    b.HasIndex(new[] { "Name" }, "clients_name_unique")
                        .IsUnique();

                    b.ToTable("clients", (string)null);
                });

            modelBuilder.Entity("CloudHub.Domain.Models.Feature", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool>("Active")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasColumnName("active")
                        .HasDefaultValueSql("true");

                    b.Property<DateTime>("CreatedOn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_on")
                        .HasDefaultValueSql("now()");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("description");

                    b.Property<Guid>("GlobalId")
                        .HasColumnType("uuid")
                        .HasColumnName("global_id");

                    b.Property<DateTime>("ModifiedOn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("modified_on")
                        .HasDefaultValueSql("now()");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("name");

                    b.Property<int>("TenantId")
                        .HasColumnType("integer")
                        .HasColumnName("tenant_id");

                    b.HasKey("Id");

                    b.HasIndex("TenantId");

                    b.HasIndex(new[] { "GlobalId" }, "features_global_id_unique")
                        .IsUnique();

                    b.HasIndex(new[] { "Name", "TenantId" }, "features_name_per_tenant_unique")
                        .IsUnique();

                    b.ToTable("features", (string)null);
                });

            modelBuilder.Entity("CloudHub.Domain.Models.Login", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("LoginTypeId")
                        .HasColumnType("integer")
                        .HasColumnName("login_type_id");

                    b.Property<string>("Passcode")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("passcode");

                    b.Property<int>("UserId")
                        .HasColumnType("integer")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.HasIndex("LoginTypeId");

                    b.HasIndex(new[] { "UserId" }, "logins_user_id_unique")
                        .IsUnique();

                    b.ToTable("logins", (string)null);
                });

            modelBuilder.Entity("CloudHub.Domain.Models.LoginType", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    b.Property<bool>("Active")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasColumnName("active")
                        .HasDefaultValueSql("true");

                    b.Property<DateTime>("CreatedOn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_on")
                        .HasDefaultValueSql("now()");

                    b.Property<DateTime>("ModifiedOn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("modified_on")
                        .HasDefaultValueSql("now()");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "Name" }, "login_types_name_unique")
                        .IsUnique();

                    b.ToTable("login_types", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1932278,
                            Active = true,
                            CreatedOn = new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc),
                            ModifiedOn = new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc),
                            Name = "Google"
                        },
                        new
                        {
                            Id = 5671293,
                            Active = true,
                            CreatedOn = new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc),
                            ModifiedOn = new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc),
                            Name = "Basic"
                        },
                        new
                        {
                            Id = 2404369,
                            Active = true,
                            CreatedOn = new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc),
                            ModifiedOn = new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc),
                            Name = "Facebook"
                        },
                        new
                        {
                            Id = 3658418,
                            Active = true,
                            CreatedOn = new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc),
                            ModifiedOn = new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc),
                            Name = "Linked In"
                        });
                });

            modelBuilder.Entity("CloudHub.Domain.Models.Nonce", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ClientId")
                        .HasColumnType("integer")
                        .HasColumnName("client_id");

                    b.Property<DateTime?>("ConsumedOn")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("consumed_on");

                    b.Property<DateTime>("CreatedOn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_on")
                        .HasDefaultValueSql("now()");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("token");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.HasIndex(new[] { "Token" }, "nonces_tokens_unique")
                        .IsUnique();

                    b.ToTable("nonces", (string)null);
                });

            modelBuilder.Entity("CloudHub.Domain.Models.PaymentGateway", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    b.Property<bool>("Active")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasColumnName("active")
                        .HasDefaultValueSql("true");

                    b.Property<DateTime>("CreatedOn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_on")
                        .HasDefaultValueSql("now()");

                    b.Property<DateTime>("ModifiedOn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("modified_on")
                        .HasDefaultValueSql("now()");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "Name" }, "payment_gateways_name_unique")
                        .IsUnique();

                    b.ToTable("payment_gateways", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1593267,
                            Active = true,
                            CreatedOn = new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc),
                            ModifiedOn = new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc),
                            Name = "Google Play Billing"
                        },
                        new
                        {
                            Id = 4863519,
                            Active = true,
                            CreatedOn = new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc),
                            ModifiedOn = new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc),
                            Name = "Paypal"
                        });
                });

            modelBuilder.Entity("CloudHub.Domain.Models.PrivateCollection", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool>("Active")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasColumnName("active")
                        .HasDefaultValueSql("true");

                    b.Property<DateTime>("CreatedOn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_on")
                        .HasDefaultValueSql("now()");

                    b.Property<DateTime>("ModifiedOn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("modified_on")
                        .HasDefaultValueSql("now()");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("name");

                    b.Property<int>("TenantId")
                        .HasColumnType("integer")
                        .HasColumnName("tenant_id");

                    b.HasKey("Id");

                    b.HasIndex("TenantId");

                    b.HasIndex(new[] { "Name" }, "private_collections_name_unique")
                        .IsUnique();

                    b.ToTable("private_collections", (string)null);
                });

            modelBuilder.Entity("CloudHub.Domain.Models.PrivateDocument", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<JsonDocument>("Body")
                        .IsRequired()
                        .HasColumnType("jsonb")
                        .HasColumnName("body");

                    b.Property<DateTime>("CreatedOn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_on")
                        .HasDefaultValueSql("now()");

                    b.Property<DateTime>("ModifiedOn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("modified_on")
                        .HasDefaultValueSql("now()");

                    b.Property<int>("PrivateCollectionId")
                        .HasColumnType("integer")
                        .HasColumnName("private_collection_id");

                    b.Property<int>("UserId")
                        .HasColumnType("integer")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.HasIndex("PrivateCollectionId");

                    b.HasIndex("UserId");

                    b.ToTable("private_documents", (string)null);
                });

            modelBuilder.Entity("CloudHub.Domain.Models.PublicCollection", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool>("Active")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasColumnName("active")
                        .HasDefaultValueSql("true");

                    b.Property<DateTime>("CreatedOn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_on")
                        .HasDefaultValueSql("now()");

                    b.Property<DateTime>("ModifiedOn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("modified_on")
                        .HasDefaultValueSql("now()");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("name");

                    b.Property<int>("TenantId")
                        .HasColumnType("integer")
                        .HasColumnName("tenant_id");

                    b.HasKey("Id");

                    b.HasIndex("TenantId");

                    b.HasIndex(new[] { "Name" }, "public_collections_name_unique")
                        .IsUnique();

                    b.ToTable("public_collections", (string)null);
                });

            modelBuilder.Entity("CloudHub.Domain.Models.PublicDocument", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<JsonDocument>("Body")
                        .IsRequired()
                        .HasColumnType("jsonb")
                        .HasColumnName("body");

                    b.Property<DateTime>("CreatedOn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_on")
                        .HasDefaultValueSql("now()");

                    b.Property<DateTime>("ModifiedOn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("modified_on")
                        .HasDefaultValueSql("now()");

                    b.Property<int>("PublicCollectionId")
                        .HasColumnType("integer")
                        .HasColumnName("public_collection_id");

                    b.HasKey("Id");

                    b.HasIndex("PublicCollectionId");

                    b.ToTable("public_documents", (string)null);
                });

            modelBuilder.Entity("CloudHub.Domain.Models.Purchase", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedOn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_on")
                        .HasDefaultValueSql("now()");

                    b.Property<int>("FeatureId")
                        .HasColumnType("integer")
                        .HasColumnName("feature_id");

                    b.Property<string>("Payload")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("payload");

                    b.Property<int>("PaymentGatewayId")
                        .HasColumnType("integer")
                        .HasColumnName("payment_gateway_id");

                    b.Property<int>("UserId")
                        .HasColumnType("integer")
                        .HasColumnName("user_id");

                    b.Property<string>("Validation")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("validation");

                    b.HasKey("Id");

                    b.HasIndex("PaymentGatewayId");

                    b.HasIndex("UserId");

                    b.HasIndex(new[] { "FeatureId", "UserId" }, "purchases_user_id_feature_id_unique")
                        .IsUnique();

                    b.ToTable("purchases", (string)null);
                });

            modelBuilder.Entity("CloudHub.Domain.Models.Tenant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool>("Active")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasColumnName("active")
                        .HasDefaultValueSql("true");

                    b.Property<DateTime>("CreatedOn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_on")
                        .HasDefaultValueSql("now()");

                    b.Property<Guid>("Guid")
                        .HasColumnType("uuid")
                        .HasColumnName("guid");

                    b.Property<DateTime>("ModifiedOn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("modified_on")
                        .HasDefaultValueSql("now()");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "Guid" }, "tenants_guid_unique")
                        .IsUnique();

                    b.HasIndex(new[] { "Name" }, "tenants_name_unique")
                        .IsUnique();

                    b.ToTable("tenants", (string)null);
                });

            modelBuilder.Entity("CloudHub.Domain.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool>("Active")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasColumnName("active")
                        .HasDefaultValueSql("true");

                    b.Property<DateTime>("CreatedOn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_on")
                        .HasDefaultValueSql("now()");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("email");

                    b.Property<string>("GlobalId")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("global_id");

                    b.Property<string>("ImageUrl")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("image_url");

                    b.Property<DateTime>("ModifiedOn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("modified_on")
                        .HasDefaultValueSql("now()");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("name");

                    b.Property<int>("TenantId")
                        .HasColumnType("integer")
                        .HasColumnName("tenant_id");

                    b.HasKey("Id");

                    b.HasIndex("TenantId");

                    b.HasIndex(new[] { "Email", "TenantId" }, "users_email_per_tenant_unique")
                        .IsUnique();

                    b.HasIndex(new[] { "GlobalId" }, "users_global_id_unique")
                        .IsUnique();

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("CloudHub.Domain.Models.UserToken", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedOn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_on")
                        .HasDefaultValueSql("now()");

                    b.Property<DateTime>("ExpiryDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("expiry_date")
                        .HasDefaultValueSql("now()");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("token");

                    b.Property<int>("UserId")
                        .HasColumnType("integer")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.HasIndex(new[] { "Token" }, "user_tokens_token_unique")
                        .IsUnique();

                    b.ToTable("user_tokens", (string)null);
                });

            modelBuilder.Entity("CloudHub.Domain.Models.Client", b =>
                {
                    b.HasOne("CloudHub.Domain.Models.Tenant", "Tenant")
                        .WithMany("Clients")
                        .HasForeignKey("TenantId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("clients_tenant_id_foreign");

                    b.Navigation("Tenant");
                });

            modelBuilder.Entity("CloudHub.Domain.Models.Feature", b =>
                {
                    b.HasOne("CloudHub.Domain.Models.Tenant", "Tenant")
                        .WithMany("Features")
                        .HasForeignKey("TenantId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("features_tenant_id_foreign");

                    b.Navigation("Tenant");
                });

            modelBuilder.Entity("CloudHub.Domain.Models.Login", b =>
                {
                    b.HasOne("CloudHub.Domain.Models.LoginType", "LoginType")
                        .WithMany("Logins")
                        .HasForeignKey("LoginTypeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("logins_login_type_id_foreign");

                    b.HasOne("CloudHub.Domain.Models.User", "User")
                        .WithOne("Login")
                        .HasForeignKey("CloudHub.Domain.Models.Login", "UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("logins_user_id_foreign");

                    b.Navigation("LoginType");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CloudHub.Domain.Models.Nonce", b =>
                {
                    b.HasOne("CloudHub.Domain.Models.Client", "Client")
                        .WithMany("Nonces")
                        .HasForeignKey("ClientId")
                        .IsRequired()
                        .HasConstraintName("nonces_client_id_foreign");

                    b.Navigation("Client");
                });

            modelBuilder.Entity("CloudHub.Domain.Models.PrivateCollection", b =>
                {
                    b.HasOne("CloudHub.Domain.Models.Tenant", "Tenant")
                        .WithMany("PrivateCollections")
                        .HasForeignKey("TenantId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("private_collections_tenant_id_foreign");

                    b.Navigation("Tenant");
                });

            modelBuilder.Entity("CloudHub.Domain.Models.PrivateDocument", b =>
                {
                    b.HasOne("CloudHub.Domain.Models.PrivateCollection", "PrivateCollection")
                        .WithMany("PrivateDocuments")
                        .HasForeignKey("PrivateCollectionId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("private_documents_private_collection_id_foreign");

                    b.HasOne("CloudHub.Domain.Models.User", "User")
                        .WithMany("PrivateDocuments")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("private_documents_user_id_foreign");

                    b.Navigation("PrivateCollection");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CloudHub.Domain.Models.PublicCollection", b =>
                {
                    b.HasOne("CloudHub.Domain.Models.Tenant", "Tenant")
                        .WithMany("PublicCollections")
                        .HasForeignKey("TenantId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("public_collections_tenant_id_foreign");

                    b.Navigation("Tenant");
                });

            modelBuilder.Entity("CloudHub.Domain.Models.PublicDocument", b =>
                {
                    b.HasOne("CloudHub.Domain.Models.PublicCollection", "PublicCollection")
                        .WithMany("PublicDocuments")
                        .HasForeignKey("PublicCollectionId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("public_documents_public_collection_id_foreign");

                    b.Navigation("PublicCollection");
                });

            modelBuilder.Entity("CloudHub.Domain.Models.Purchase", b =>
                {
                    b.HasOne("CloudHub.Domain.Models.Feature", "Feature")
                        .WithMany("Purchases")
                        .HasForeignKey("FeatureId")
                        .IsRequired()
                        .HasConstraintName("purchases_feature_id_foreign");

                    b.HasOne("CloudHub.Domain.Models.PaymentGateway", "PaymentGateway")
                        .WithMany("Purchases")
                        .HasForeignKey("PaymentGatewayId")
                        .IsRequired()
                        .HasConstraintName("purchases_payment_gateway_id_foreign");

                    b.HasOne("CloudHub.Domain.Models.User", "User")
                        .WithMany("Purchases")
                        .HasForeignKey("UserId")
                        .IsRequired()
                        .HasConstraintName("purchases_user_id_foreign");

                    b.Navigation("Feature");

                    b.Navigation("PaymentGateway");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CloudHub.Domain.Models.User", b =>
                {
                    b.HasOne("CloudHub.Domain.Models.Tenant", "Tenant")
                        .WithMany("Users")
                        .HasForeignKey("TenantId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("users_tenant_id_foreign");

                    b.Navigation("Tenant");
                });

            modelBuilder.Entity("CloudHub.Domain.Models.UserToken", b =>
                {
                    b.HasOne("CloudHub.Domain.Models.User", "User")
                        .WithMany("UserTokens")
                        .HasForeignKey("UserId")
                        .IsRequired()
                        .HasConstraintName("user_tokens_user_id_foreign");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CloudHub.Domain.Models.Client", b =>
                {
                    b.Navigation("Nonces");
                });

            modelBuilder.Entity("CloudHub.Domain.Models.Feature", b =>
                {
                    b.Navigation("Purchases");
                });

            modelBuilder.Entity("CloudHub.Domain.Models.LoginType", b =>
                {
                    b.Navigation("Logins");
                });

            modelBuilder.Entity("CloudHub.Domain.Models.PaymentGateway", b =>
                {
                    b.Navigation("Purchases");
                });

            modelBuilder.Entity("CloudHub.Domain.Models.PrivateCollection", b =>
                {
                    b.Navigation("PrivateDocuments");
                });

            modelBuilder.Entity("CloudHub.Domain.Models.PublicCollection", b =>
                {
                    b.Navigation("PublicDocuments");
                });

            modelBuilder.Entity("CloudHub.Domain.Models.Tenant", b =>
                {
                    b.Navigation("Clients");

                    b.Navigation("Features");

                    b.Navigation("PrivateCollections");

                    b.Navigation("PublicCollections");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("CloudHub.Domain.Models.User", b =>
                {
                    b.Navigation("Login")
                        .IsRequired();

                    b.Navigation("PrivateDocuments");

                    b.Navigation("Purchases");

                    b.Navigation("UserTokens");
                });
#pragma warning restore 612, 618
        }
    }
}
