﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CloudHub.Infra.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "applications",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    guid = table.Column<Guid>(type: "uuid", nullable: false),
                    modified_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    active = table.Column<bool>(type: "boolean", nullable: false, defaultValueSql: "true")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_applications", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "client_types",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    modified_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    active = table.Column<bool>(type: "boolean", nullable: false, defaultValueSql: "true")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_client_types", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "login_types",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    active = table.Column<bool>(type: "boolean", nullable: false, defaultValueSql: "true")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_login_types", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "payment_gateways",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    modified_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    active = table.Column<bool>(type: "boolean", nullable: false, defaultValueSql: "true")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_payment_gateways", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "actions",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    category = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    description = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    payload = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    app_version = table.Column<int>(type: "integer", nullable: true),
                    application_id = table.Column<int>(type: "integer", nullable: false),
                    created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_actions", x => x.id);
                    table.ForeignKey(
                        name: "actions_application_id_foreign",
                        column: x => x.application_id,
                        principalTable: "applications",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "application_secrets",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    secret_key = table.Column<string>(type: "text", nullable: false),
                    secret_value = table.Column<string>(type: "text", nullable: false),
                    application_id = table.Column<int>(type: "integer", nullable: false),
                    modified_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    active = table.Column<bool>(type: "boolean", nullable: false, defaultValueSql: "true")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_application_secrets", x => x.id);
                    table.ForeignKey(
                        name: "application_secrets_application_id_foreign",
                        column: x => x.application_id,
                        principalTable: "applications",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "features",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    global_id = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    description = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    application_id = table.Column<int>(type: "integer", nullable: false),
                    modified_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    active = table.Column<bool>(type: "boolean", nullable: false, defaultValueSql: "true")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_features", x => x.id);
                    table.ForeignKey(
                        name: "features_app_id_foreign",
                        column: x => x.application_id,
                        principalTable: "applications",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "releases",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    release_name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    notes = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    application_id = table.Column<int>(type: "integer", nullable: false),
                    release_date = table.Column<DateOnly>(type: "date", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_releases", x => x.id);
                    table.ForeignKey(
                        name: "releases_app_id_foreign",
                        column: x => x.application_id,
                        principalTable: "applications",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    image_url = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    global_id = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    application_id = table.Column<int>(type: "integer", nullable: false),
                    modified_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    active = table.Column<bool>(type: "boolean", nullable: false, defaultValueSql: "true")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                    table.ForeignKey(
                        name: "users_application_id_foreign",
                        column: x => x.application_id,
                        principalTable: "applications",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "clients",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    client_key = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    client_secret = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    client_type_id = table.Column<int>(type: "integer", nullable: false),
                    modified_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    active = table.Column<bool>(type: "boolean", nullable: false, defaultValueSql: "true")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_clients", x => x.id);
                    table.ForeignKey(
                        name: "clients_client_type_id_foreign",
                        column: x => x.client_type_id,
                        principalTable: "client_types",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "logins",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    passcode = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    login_type_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_logins", x => x.id);
                    table.ForeignKey(
                        name: "logins_login_type_id_foreign",
                        column: x => x.login_type_id,
                        principalTable: "login_types",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "logins_user_id_foreign",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "purchases",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    feature_id = table.Column<int>(type: "integer", nullable: false),
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    payment_gateway_id = table.Column<int>(type: "integer", nullable: false),
                    payload = table.Column<string>(type: "text", nullable: false),
                    validation = table.Column<string>(type: "text", nullable: false),
                    created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_purchases", x => x.id);
                    table.ForeignKey(
                        name: "purchases_feature_id_foreign",
                        column: x => x.feature_id,
                        principalTable: "features",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "purchases_payment_gateway_id_foreign",
                        column: x => x.payment_gateway_id,
                        principalTable: "payment_gateways",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "purchases_user_id_foreign",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "user_tokens",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    token = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    expiry_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    active = table.Column<bool>(type: "boolean", nullable: false, defaultValueSql: "true")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_tokens", x => x.id);
                    table.ForeignKey(
                        name: "user_tokens_user_id_foreign",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "clients_applications",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    application_id = table.Column<int>(type: "integer", nullable: false),
                    client_id = table.Column<int>(type: "integer", nullable: false),
                    active = table.Column<bool>(type: "boolean", nullable: false, defaultValueSql: "true")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_clients_applications", x => x.id);
                    table.ForeignKey(
                        name: "clients_applications_application_id_foreign",
                        column: x => x.application_id,
                        principalTable: "applications",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "clients_applications_client_id_foreign",
                        column: x => x.client_id,
                        principalTable: "clients",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "nonces",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    application_id = table.Column<int>(type: "integer", nullable: false),
                    token = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    client_id = table.Column<int>(type: "integer", nullable: false),
                    consumed_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_nonces", x => x.id);
                    table.ForeignKey(
                        name: "nonces_application_id_foreign",
                        column: x => x.application_id,
                        principalTable: "applications",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "nonces_client_id_foreign",
                        column: x => x.client_id,
                        principalTable: "clients",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_actions_application_id",
                table: "actions",
                column: "application_id");

            migrationBuilder.CreateIndex(
                name: "IX_application_secrets_application_id",
                table: "application_secrets",
                column: "application_id");

            migrationBuilder.CreateIndex(
                name: "applications_guid_unique",
                table: "applications",
                column: "guid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "clients_client_key_unique",
                table: "clients",
                column: "client_key",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "clients_client_secret_unique",
                table: "clients",
                column: "client_secret",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_clients_client_type_id",
                table: "clients",
                column: "client_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_clients_applications_application_id",
                table: "clients_applications",
                column: "application_id");

            migrationBuilder.CreateIndex(
                name: "IX_clients_applications_client_id",
                table: "clients_applications",
                column: "client_id");

            migrationBuilder.CreateIndex(
                name: "features_global_id_unique",
                table: "features",
                columns: new[] { "application_id", "global_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_logins_login_type_id",
                table: "logins",
                column: "login_type_id");

            migrationBuilder.CreateIndex(
                name: "logins_user_id_unique",
                table: "logins",
                column: "user_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_nonces_application_id",
                table: "nonces",
                column: "application_id");

            migrationBuilder.CreateIndex(
                name: "IX_nonces_client_id",
                table: "nonces",
                column: "client_id");

            migrationBuilder.CreateIndex(
                name: "nonces_tokens_unique",
                table: "nonces",
                column: "token",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_purchases_payment_gateway_id",
                table: "purchases",
                column: "payment_gateway_id");

            migrationBuilder.CreateIndex(
                name: "IX_purchases_user_id",
                table: "purchases",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "purchases_user_id_feature_id_unique",
                table: "purchases",
                columns: new[] { "feature_id", "user_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "releases_name_unique",
                table: "releases",
                columns: new[] { "application_id", "release_name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_user_tokens_user_id",
                table: "user_tokens",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "user_tokens_token_unique",
                table: "user_tokens",
                column: "token",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_users_application_id",
                table: "users",
                column: "application_id");

            migrationBuilder.CreateIndex(
                name: "users_email_unique",
                table: "users",
                columns: new[] { "email", "application_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "users_global_id_unique",
                table: "users",
                column: "global_id",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "actions");

            migrationBuilder.DropTable(
                name: "application_secrets");

            migrationBuilder.DropTable(
                name: "clients_applications");

            migrationBuilder.DropTable(
                name: "logins");

            migrationBuilder.DropTable(
                name: "nonces");

            migrationBuilder.DropTable(
                name: "purchases");

            migrationBuilder.DropTable(
                name: "releases");

            migrationBuilder.DropTable(
                name: "user_tokens");

            migrationBuilder.DropTable(
                name: "login_types");

            migrationBuilder.DropTable(
                name: "clients");

            migrationBuilder.DropTable(
                name: "features");

            migrationBuilder.DropTable(
                name: "payment_gateways");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "client_types");

            migrationBuilder.DropTable(
                name: "applications");
        }
    }
}