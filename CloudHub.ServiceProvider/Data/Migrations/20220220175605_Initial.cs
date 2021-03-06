using System;
using System.Text.Json;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CloudHub.ServiceProvider.Data
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "login_types",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    active = table.Column<bool>(type: "boolean", nullable: false, defaultValueSql: "true"),
                    modified_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
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
                    active = table.Column<bool>(type: "boolean", nullable: false, defaultValueSql: "true"),
                    modified_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_payment_gateways", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tenants",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    guid = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    active = table.Column<bool>(type: "boolean", nullable: false, defaultValueSql: "true"),
                    modified_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tenants", x => x.id);
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
                    tenant_id = table.Column<int>(type: "integer", nullable: false),
                    active = table.Column<bool>(type: "boolean", nullable: false, defaultValueSql: "true"),
                    modified_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_clients", x => x.id);
                    table.ForeignKey(
                        name: "clients_tenant_id_foreign",
                        column: x => x.tenant_id,
                        principalTable: "tenants",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "features",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    global_id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    description = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    tenant_id = table.Column<int>(type: "integer", nullable: false),
                    active = table.Column<bool>(type: "boolean", nullable: false, defaultValueSql: "true"),
                    modified_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_features", x => x.id);
                    table.ForeignKey(
                        name: "features_tenant_id_foreign",
                        column: x => x.tenant_id,
                        principalTable: "tenants",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "private_collections",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    active = table.Column<bool>(type: "boolean", nullable: false, defaultValueSql: "true"),
                    tenant_id = table.Column<int>(type: "integer", nullable: false),
                    modified_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_private_collections", x => x.id);
                    table.ForeignKey(
                        name: "private_collections_tenant_id_foreign",
                        column: x => x.tenant_id,
                        principalTable: "tenants",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "public_collections",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    tenant_id = table.Column<int>(type: "integer", nullable: false),
                    active = table.Column<bool>(type: "boolean", nullable: false, defaultValueSql: "true"),
                    modified_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_public_collections", x => x.id);
                    table.ForeignKey(
                        name: "public_collections_tenant_id_foreign",
                        column: x => x.tenant_id,
                        principalTable: "tenants",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
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
                    tenant_id = table.Column<int>(type: "integer", nullable: false),
                    active = table.Column<bool>(type: "boolean", nullable: false, defaultValueSql: "true"),
                    modified_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                    table.ForeignKey(
                        name: "users_tenant_id_foreign",
                        column: x => x.tenant_id,
                        principalTable: "tenants",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "nonces",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    token = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    client_id = table.Column<int>(type: "integer", nullable: false),
                    consumed_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_nonces", x => x.id);
                    table.ForeignKey(
                        name: "nonces_client_id_foreign",
                        column: x => x.client_id,
                        principalTable: "clients",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "public_documents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    body = table.Column<JsonDocument>(type: "jsonb", nullable: false),
                    public_collection_id = table.Column<int>(type: "integer", nullable: false),
                    modified_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_public_documents", x => x.Id);
                    table.ForeignKey(
                        name: "public_documents_public_collection_id_foreign",
                        column: x => x.public_collection_id,
                        principalTable: "public_collections",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
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
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "logins_user_id_foreign",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "private_documents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    body = table.Column<JsonDocument>(type: "jsonb", nullable: false),
                    private_collection_id = table.Column<int>(type: "integer", nullable: false),
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    modified_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_private_documents", x => x.Id);
                    table.ForeignKey(
                        name: "private_documents_private_collection_id_foreign",
                        column: x => x.private_collection_id,
                        principalTable: "private_collections",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "private_documents_user_id_foreign",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
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
                    expiry_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
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

            migrationBuilder.InsertData(
                table: "login_types",
                columns: new[] { "id", "active", "created_on", "modified_on", "name" },
                values: new object[,]
                {
                    { 1932278, true, new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Google" },
                    { 2404369, true, new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Facebook" },
                    { 3658418, true, new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Linked In" },
                    { 5671293, true, new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Basic" }
                });

            migrationBuilder.InsertData(
                table: "payment_gateways",
                columns: new[] { "id", "active", "created_on", "modified_on", "name" },
                values: new object[,]
                {
                    { 1593267, true, new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Google Play Billing" },
                    { 4863519, true, new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Paypal" }
                });

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
                name: "clients_name_unique",
                table: "clients",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_clients_tenant_id",
                table: "clients",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "features_global_id_unique",
                table: "features",
                column: "global_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "features_name_per_tenant_unique",
                table: "features",
                columns: new[] { "name", "tenant_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_features_tenant_id",
                table: "features",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "login_types_name_unique",
                table: "login_types",
                column: "name",
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
                name: "IX_nonces_client_id",
                table: "nonces",
                column: "client_id");

            migrationBuilder.CreateIndex(
                name: "nonces_tokens_unique",
                table: "nonces",
                column: "token",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "payment_gateways_name_unique",
                table: "payment_gateways",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_private_collections_tenant_id",
                table: "private_collections",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "private_collections_name_unique",
                table: "private_collections",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_private_documents_private_collection_id",
                table: "private_documents",
                column: "private_collection_id");

            migrationBuilder.CreateIndex(
                name: "IX_private_documents_user_id",
                table: "private_documents",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_public_collections_tenant_id",
                table: "public_collections",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "public_collections_name_unique",
                table: "public_collections",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_public_documents_public_collection_id",
                table: "public_documents",
                column: "public_collection_id");

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
                name: "tenants_guid_unique",
                table: "tenants",
                column: "guid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "tenants_name_unique",
                table: "tenants",
                column: "name",
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
                name: "IX_users_tenant_id",
                table: "users",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "users_email_per_tenant_unique",
                table: "users",
                columns: new[] { "email", "tenant_id" },
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
                name: "logins");

            migrationBuilder.DropTable(
                name: "nonces");

            migrationBuilder.DropTable(
                name: "private_documents");

            migrationBuilder.DropTable(
                name: "public_documents");

            migrationBuilder.DropTable(
                name: "purchases");

            migrationBuilder.DropTable(
                name: "user_tokens");

            migrationBuilder.DropTable(
                name: "login_types");

            migrationBuilder.DropTable(
                name: "clients");

            migrationBuilder.DropTable(
                name: "private_collections");

            migrationBuilder.DropTable(
                name: "public_collections");

            migrationBuilder.DropTable(
                name: "features");

            migrationBuilder.DropTable(
                name: "payment_gateways");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "tenants");
        }
    }
}
