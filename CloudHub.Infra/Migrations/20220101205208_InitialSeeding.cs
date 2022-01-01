using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CloudHub.Infra.Migrations
{
    public partial class InitialSeeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "client_types",
                columns: new[] { "id", "active", "name" },
                values: new object[,]
                {
                    { 7061601, true, "Admin" },
                    { 38359937, true, "Application" },
                    { 41596505, true, "Dashboard" }
                });

            migrationBuilder.InsertData(
                table: "login_types",
                columns: new[] { "id", "active", "name" },
                values: new object[,]
                {
                    { 1932278, true, "Google" },
                    { 2404369, true, "Facebook" },
                    { 3658418, true, "Linked In" },
                    { 5671293, true, "Basic" }
                });

            migrationBuilder.InsertData(
                table: "payment_gateways",
                columns: new[] { "id", "active", "name" },
                values: new object[,]
                {
                    { 1593267, true, "Google Play Billing" },
                    { 4863519, true, "Paypal" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "client_types",
                keyColumn: "id",
                keyValue: 7061601);

            migrationBuilder.DeleteData(
                table: "client_types",
                keyColumn: "id",
                keyValue: 38359937);

            migrationBuilder.DeleteData(
                table: "client_types",
                keyColumn: "id",
                keyValue: 41596505);

            migrationBuilder.DeleteData(
                table: "login_types",
                keyColumn: "id",
                keyValue: 1932278);

            migrationBuilder.DeleteData(
                table: "login_types",
                keyColumn: "id",
                keyValue: 2404369);

            migrationBuilder.DeleteData(
                table: "login_types",
                keyColumn: "id",
                keyValue: 3658418);

            migrationBuilder.DeleteData(
                table: "login_types",
                keyColumn: "id",
                keyValue: 5671293);

            migrationBuilder.DeleteData(
                table: "payment_gateways",
                keyColumn: "id",
                keyValue: 1593267);

            migrationBuilder.DeleteData(
                table: "payment_gateways",
                keyColumn: "id",
                keyValue: 4863519);
        }
    }
}
