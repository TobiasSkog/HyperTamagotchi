using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HyperTamagotchi_API.Migrations.AuthDb
{
    /// <inheritdoc />
    public partial class mssqllocal_migration_537 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0feb61fe-b453-41cd-a19f-3336a49cc33a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4187c93d-9325-41c0-aca3-7bdd5d4fa5e4");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0eea1c7f-d1a3-4fdf-be59-cf1d44551f22", null, "Customer", "CUSTOMER" },
                    { "f1690041-63a8-44ca-83a4-3ea44f8454cd", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0eea1c7f-d1a3-4fdf-be59-cf1d44551f22");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f1690041-63a8-44ca-83a4-3ea44f8454cd");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0feb61fe-b453-41cd-a19f-3336a49cc33a", null, "Customer", "WRITER" },
                    { "4187c93d-9325-41c0-aca3-7bdd5d4fa5e4", null, "Admin", "READER" }
                });
        }
    }
}
