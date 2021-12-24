﻿// <auto-generated />
using System;
using CloudHub.Infra.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CloudHub.Infra.Migrations
{
    [DbContext(typeof(MyDbContext))]
    partial class MyDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CloudHub.Domain.Entities.Application", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool?>("Active")
                        .IsRequired()
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

                    b.HasIndex(new[] { "Guid" }, "applications_guid_unique")
                        .IsUnique();

                    b.ToTable("applications", (string)null);
                });

            modelBuilder.Entity("CloudHub.Domain.Entities.ApplicationSecret", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool?>("Active")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasColumnName("active")
                        .HasDefaultValueSql("true");

                    b.Property<int>("ApplicationId")
                        .HasColumnType("integer")
                        .HasColumnName("application_id");

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

                    b.Property<string>("SecretKey")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("secret_key");

                    b.Property<string>("SecretValue")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("secret_value");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationId");

                    b.ToTable("application_secrets", (string)null);
                });

            modelBuilder.Entity("CloudHub.Domain.Entities.Client", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool?>("Active")
                        .IsRequired()
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

                    b.Property<int>("ClientTypeId")
                        .HasColumnType("integer")
                        .HasColumnName("client_type_id");

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

                    b.HasIndex("ClientTypeId");

                    b.HasIndex(new[] { "ClientKey" }, "clients_client_key_unique")
                        .IsUnique();

                    b.HasIndex(new[] { "ClientSecret" }, "clients_client_secret_unique")
                        .IsUnique();

                    b.ToTable("clients", (string)null);
                });

            modelBuilder.Entity("CloudHub.Domain.Entities.ClientApplicationRelation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool?>("Active")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasColumnName("active")
                        .HasDefaultValueSql("true");

                    b.Property<int>("ApplicationId")
                        .HasColumnType("integer")
                        .HasColumnName("application_id");

                    b.Property<int>("ClientId")
                        .HasColumnType("integer")
                        .HasColumnName("client_id");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationId");

                    b.HasIndex("ClientId");

                    b.ToTable("clients_applications", (string)null);
                });

            modelBuilder.Entity("CloudHub.Domain.Entities.ClientType", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    b.Property<bool?>("Active")
                        .IsRequired()
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

                    b.ToTable("client_types", (string)null);
                });

            modelBuilder.Entity("CloudHub.Domain.Entities.Feature", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool?>("Active")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasColumnName("active")
                        .HasDefaultValueSql("true");

                    b.Property<int>("ApplicationId")
                        .HasColumnType("integer")
                        .HasColumnName("application_id");

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

                    b.Property<string>("GlobalId")
                        .IsRequired()
                        .HasColumnType("text")
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

                    b.HasKey("Id");

                    b.HasIndex(new[] { "ApplicationId", "GlobalId" }, "features_global_id_unique")
                        .IsUnique();

                    b.ToTable("features", (string)null);
                });

            modelBuilder.Entity("CloudHub.Domain.Entities.Login", b =>
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

            modelBuilder.Entity("CloudHub.Domain.Entities.LoginType", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    b.Property<bool?>("Active")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasColumnName("active")
                        .HasDefaultValueSql("true");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("login_types", (string)null);
                });

            modelBuilder.Entity("CloudHub.Domain.Entities.Nonce", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ApplicationId")
                        .HasColumnType("integer")
                        .HasColumnName("application_id");

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

                    b.HasIndex("ApplicationId");

                    b.HasIndex("ClientId");

                    b.HasIndex(new[] { "Token" }, "nonces_tokens_unique")
                        .IsUnique();

                    b.ToTable("nonces", (string)null);
                });

            modelBuilder.Entity("CloudHub.Domain.Entities.PaymentGateway", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    b.Property<bool?>("Active")
                        .IsRequired()
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

                    b.ToTable("payment_gateways", (string)null);
                });

            modelBuilder.Entity("CloudHub.Domain.Entities.Purchase", b =>
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

            modelBuilder.Entity("CloudHub.Domain.Entities.Release", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ApplicationId")
                        .HasColumnType("integer")
                        .HasColumnName("application_id");

                    b.Property<string>("Notes")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("notes");

                    b.Property<DateOnly>("ReleaseDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("date")
                        .HasColumnName("release_date")
                        .HasDefaultValueSql("now()");

                    b.Property<string>("ReleaseName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("release_name");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "ApplicationId", "ReleaseName" }, "releases_name_unique")
                        .IsUnique();

                    b.ToTable("releases", (string)null);
                });

            modelBuilder.Entity("CloudHub.Domain.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool?>("Active")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasColumnName("active")
                        .HasDefaultValueSql("true");

                    b.Property<int>("ApplicationId")
                        .HasColumnType("integer")
                        .HasColumnName("application_id");

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

                    b.HasKey("Id");

                    b.HasIndex("ApplicationId");

                    b.HasIndex(new[] { "Email", "ApplicationId" }, "users_email_unique")
                        .IsUnique();

                    b.HasIndex(new[] { "GlobalId" }, "users_global_id_unique")
                        .IsUnique();

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("CloudHub.Domain.Entities.UserAction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("AppVersion")
                        .HasColumnType("integer")
                        .HasColumnName("app_version");

                    b.Property<int>("ApplicationId")
                        .HasColumnType("integer")
                        .HasColumnName("application_id");

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("category");

                    b.Property<DateTime>("CreatedOn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_on")
                        .HasDefaultValueSql("now()");

                    b.Property<string>("Description")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("description");

                    b.Property<string>("Payload")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("payload");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationId");

                    b.ToTable("actions", (string)null);
                });

            modelBuilder.Entity("CloudHub.Domain.Entities.UserToken", b =>
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

            modelBuilder.Entity("CloudHub.Domain.Entities.ApplicationSecret", b =>
                {
                    b.HasOne("CloudHub.Domain.Entities.Application", "Application")
                        .WithMany("ApplicationSecrets")
                        .HasForeignKey("ApplicationId")
                        .IsRequired()
                        .HasConstraintName("application_secrets_application_id_foreign");

                    b.Navigation("Application");
                });

            modelBuilder.Entity("CloudHub.Domain.Entities.Client", b =>
                {
                    b.HasOne("CloudHub.Domain.Entities.ClientType", "ClientType")
                        .WithMany("Clients")
                        .HasForeignKey("ClientTypeId")
                        .IsRequired()
                        .HasConstraintName("clients_client_type_id_foreign");

                    b.Navigation("ClientType");
                });

            modelBuilder.Entity("CloudHub.Domain.Entities.ClientApplicationRelation", b =>
                {
                    b.HasOne("CloudHub.Domain.Entities.Application", "Application")
                        .WithMany("ClientsApplications")
                        .HasForeignKey("ApplicationId")
                        .IsRequired()
                        .HasConstraintName("clients_applications_application_id_foreign");

                    b.HasOne("CloudHub.Domain.Entities.Client", "Client")
                        .WithMany("ClientsApplications")
                        .HasForeignKey("ClientId")
                        .IsRequired()
                        .HasConstraintName("clients_applications_client_id_foreign");

                    b.Navigation("Application");

                    b.Navigation("Client");
                });

            modelBuilder.Entity("CloudHub.Domain.Entities.Feature", b =>
                {
                    b.HasOne("CloudHub.Domain.Entities.Application", "Application")
                        .WithMany("Features")
                        .HasForeignKey("ApplicationId")
                        .IsRequired()
                        .HasConstraintName("features_app_id_foreign");

                    b.Navigation("Application");
                });

            modelBuilder.Entity("CloudHub.Domain.Entities.Login", b =>
                {
                    b.HasOne("CloudHub.Domain.Entities.LoginType", "LoginType")
                        .WithMany("Logins")
                        .HasForeignKey("LoginTypeId")
                        .IsRequired()
                        .HasConstraintName("logins_login_type_id_foreign");

                    b.HasOne("CloudHub.Domain.Entities.User", "User")
                        .WithOne("Login")
                        .HasForeignKey("CloudHub.Domain.Entities.Login", "UserId")
                        .IsRequired()
                        .HasConstraintName("logins_user_id_foreign");

                    b.Navigation("LoginType");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CloudHub.Domain.Entities.Nonce", b =>
                {
                    b.HasOne("CloudHub.Domain.Entities.Application", "Application")
                        .WithMany("Nonces")
                        .HasForeignKey("ApplicationId")
                        .IsRequired()
                        .HasConstraintName("nonces_application_id_foreign");

                    b.HasOne("CloudHub.Domain.Entities.Client", "Client")
                        .WithMany("Nonces")
                        .HasForeignKey("ClientId")
                        .IsRequired()
                        .HasConstraintName("nonces_client_id_foreign");

                    b.Navigation("Application");

                    b.Navigation("Client");
                });

            modelBuilder.Entity("CloudHub.Domain.Entities.Purchase", b =>
                {
                    b.HasOne("CloudHub.Domain.Entities.Feature", "Feature")
                        .WithMany("Purchases")
                        .HasForeignKey("FeatureId")
                        .IsRequired()
                        .HasConstraintName("purchases_feature_id_foreign");

                    b.HasOne("CloudHub.Domain.Entities.PaymentGateway", "PaymentGateway")
                        .WithMany("Purchases")
                        .HasForeignKey("PaymentGatewayId")
                        .IsRequired()
                        .HasConstraintName("purchases_payment_gateway_id_foreign");

                    b.HasOne("CloudHub.Domain.Entities.User", "User")
                        .WithMany("Purchases")
                        .HasForeignKey("UserId")
                        .IsRequired()
                        .HasConstraintName("purchases_user_id_foreign");

                    b.Navigation("Feature");

                    b.Navigation("PaymentGateway");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CloudHub.Domain.Entities.Release", b =>
                {
                    b.HasOne("CloudHub.Domain.Entities.Application", "Application")
                        .WithMany("Releases")
                        .HasForeignKey("ApplicationId")
                        .IsRequired()
                        .HasConstraintName("releases_app_id_foreign");

                    b.Navigation("Application");
                });

            modelBuilder.Entity("CloudHub.Domain.Entities.User", b =>
                {
                    b.HasOne("CloudHub.Domain.Entities.Application", "Application")
                        .WithMany("Users")
                        .HasForeignKey("ApplicationId")
                        .IsRequired()
                        .HasConstraintName("users_application_id_foreign");

                    b.Navigation("Application");
                });

            modelBuilder.Entity("CloudHub.Domain.Entities.UserAction", b =>
                {
                    b.HasOne("CloudHub.Domain.Entities.Application", "Application")
                        .WithMany("Actions")
                        .HasForeignKey("ApplicationId")
                        .IsRequired()
                        .HasConstraintName("actions_application_id_foreign");

                    b.Navigation("Application");
                });

            modelBuilder.Entity("CloudHub.Domain.Entities.UserToken", b =>
                {
                    b.HasOne("CloudHub.Domain.Entities.User", "User")
                        .WithMany("UserTokens")
                        .HasForeignKey("UserId")
                        .IsRequired()
                        .HasConstraintName("user_tokens_user_id_foreign");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CloudHub.Domain.Entities.Application", b =>
                {
                    b.Navigation("Actions");

                    b.Navigation("ApplicationSecrets");

                    b.Navigation("ClientsApplications");

                    b.Navigation("Features");

                    b.Navigation("Nonces");

                    b.Navigation("Releases");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("CloudHub.Domain.Entities.Client", b =>
                {
                    b.Navigation("ClientsApplications");

                    b.Navigation("Nonces");
                });

            modelBuilder.Entity("CloudHub.Domain.Entities.ClientType", b =>
                {
                    b.Navigation("Clients");
                });

            modelBuilder.Entity("CloudHub.Domain.Entities.Feature", b =>
                {
                    b.Navigation("Purchases");
                });

            modelBuilder.Entity("CloudHub.Domain.Entities.LoginType", b =>
                {
                    b.Navigation("Logins");
                });

            modelBuilder.Entity("CloudHub.Domain.Entities.PaymentGateway", b =>
                {
                    b.Navigation("Purchases");
                });

            modelBuilder.Entity("CloudHub.Domain.Entities.User", b =>
                {
                    b.Navigation("Login")
                        .IsRequired();

                    b.Navigation("Purchases");

                    b.Navigation("UserTokens");
                });
#pragma warning restore 612, 618
        }
    }
}
