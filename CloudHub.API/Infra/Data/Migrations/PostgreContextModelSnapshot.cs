﻿// <auto-generated />
using System;
using CloudHub.Infra.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CloudHub.Infra.Data.Migrations
{
    [DbContext(typeof(PostgreContext))]
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
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool>("Active")
                        .HasColumnType("boolean");

                    b.Property<string>("ClientKey")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ClientSecret")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("TenantId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("TenantId");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("CloudHub.Domain.Models.Feature", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool>("Active")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("GlobalId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("TenantId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("TenantId");

                    b.ToTable("Features");
                });

            modelBuilder.Entity("CloudHub.Domain.Models.Login", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("LoginTypeId")
                        .HasColumnType("integer");

                    b.Property<string>("Passcode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("LoginTypeId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Logins");
                });

            modelBuilder.Entity("CloudHub.Domain.Models.LoginType", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("integer");

                    b.Property<bool>("Active")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("LoginTypes");

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
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ClientId")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("ConsumedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.ToTable("Nonces");
                });

            modelBuilder.Entity("CloudHub.Domain.Models.PaymentGateway", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("integer");

                    b.Property<bool>("Active")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("PaymentGateways");

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

            modelBuilder.Entity("CloudHub.Domain.Models.Purchase", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("FeatureId")
                        .HasColumnType("integer");

                    b.Property<string>("Payload")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("PaymentGatewayId")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<string>("Validation")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("FeatureId");

                    b.HasIndex("PaymentGatewayId");

                    b.HasIndex("UserId");

                    b.ToTable("Purchases");
                });

            modelBuilder.Entity("CloudHub.Domain.Models.Tenant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool>("Active")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("Guid")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Tenants");
                });

            modelBuilder.Entity("CloudHub.Domain.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool>("Active")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("GlobalId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("text");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("TenantId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("TenantId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("CloudHub.Domain.Models.UserToken", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("ExpiryDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserTokens");
                });

            modelBuilder.Entity("CloudHub.Domain.Models.Client", b =>
                {
                    b.HasOne("CloudHub.Domain.Models.Tenant", "Tenant")
                        .WithMany("Clients")
                        .HasForeignKey("TenantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tenant");
                });

            modelBuilder.Entity("CloudHub.Domain.Models.Feature", b =>
                {
                    b.HasOne("CloudHub.Domain.Models.Tenant", "Tenant")
                        .WithMany("Features")
                        .HasForeignKey("TenantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tenant");
                });

            modelBuilder.Entity("CloudHub.Domain.Models.Login", b =>
                {
                    b.HasOne("CloudHub.Domain.Models.LoginType", "LoginType")
                        .WithMany("Logins")
                        .HasForeignKey("LoginTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CloudHub.Domain.Models.User", "User")
                        .WithOne("Login")
                        .HasForeignKey("CloudHub.Domain.Models.Login", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("LoginType");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CloudHub.Domain.Models.Nonce", b =>
                {
                    b.HasOne("CloudHub.Domain.Models.Client", "Client")
                        .WithMany("Nonces")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");
                });

            modelBuilder.Entity("CloudHub.Domain.Models.Purchase", b =>
                {
                    b.HasOne("CloudHub.Domain.Models.Feature", "Feature")
                        .WithMany("Purchases")
                        .HasForeignKey("FeatureId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CloudHub.Domain.Models.PaymentGateway", "PaymentGateway")
                        .WithMany("Purchases")
                        .HasForeignKey("PaymentGatewayId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CloudHub.Domain.Models.User", "User")
                        .WithMany("Purchases")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Feature");

                    b.Navigation("PaymentGateway");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CloudHub.Domain.Models.User", b =>
                {
                    b.HasOne("CloudHub.Domain.Models.Tenant", "Tenant")
                        .WithMany("Users")
                        .HasForeignKey("TenantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tenant");
                });

            modelBuilder.Entity("CloudHub.Domain.Models.UserToken", b =>
                {
                    b.HasOne("CloudHub.Domain.Models.User", "User")
                        .WithMany("UserTokens")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

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

            modelBuilder.Entity("CloudHub.Domain.Models.Tenant", b =>
                {
                    b.Navigation("Clients");

                    b.Navigation("Features");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("CloudHub.Domain.Models.User", b =>
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
