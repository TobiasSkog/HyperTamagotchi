using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HyperTamagotchi_API.Migrations
{
    /// <inheritdoc />
    public partial class mssqllocal_migration_REFRESH_TOKENS : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "067028e6-3218-4632-9086-6e5a3eac11a3", "43218869-d83d-4e9c-b19e-c1adc1da1453" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "0793c5be-2c8a-4aee-abe9-97127838911f", "fdd69357-45d0-4f57-a4ed-555d7e5df98b" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "067028e6-3218-4632-9086-6e5a3eac11a3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0793c5be-2c8a-4aee-abe9-97127838911f");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "43218869-d83d-4e9c-b19e-c1adc1da1453");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "fdd69357-45d0-4f57-a4ed-555d7e5df98b");

            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0a02ad2d-9ab6-45c9-938a-4607226f7342", null, "Customer", "CUSTOMER" },
                    { "b2b87818-8027-4da0-afe5-ad5712d0d65a", null, "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "AddressId", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RefreshToken", "SecurityStamp", "ShoppingCartId", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "04b60415-7244-4bd2-9870-7f1efca14e71", 0, 1, "605d459a-888e-4daf-a4cd-6087554fe07f", "Customer", "admin@gmail.com", true, "Admin", "Adminsson", true, null, "ADMIN@GMAIL.COM", "ADMIN@GMAIL.COM", "AQAAAAIAAYagAAAAEN68ZWryxfY44dlBMdEwJyq+jJYWH1RjRUMlkTFFumnw3U+WemCLRctElbxY9nIP+A==", "1234567890", true, null, "641ccecc-d89b-4f80-b627-1eba30c8ed85", 1, false, "admin@gmail.com" },
                    { "12f8e39b-32ad-408f-bda7-8c234fd54cf6", 0, 2, "6587afcf-0493-4873-b6ff-74c97c25afa5", "Customer", "tobias@gmail.com", true, "Tobias", "Skog", true, null, "TOBIAS@GMAIL.COM", "TOBIAS@GMAIL.COM", "AQAAAAIAAYagAAAAEN7FqRHULNbj83m6dHDELuUxFKR9Yh2FzFQgHJQvj1yqLEXmTWqDSxgHwGAii/sZ2Q==", "1234567890", true, null, "d97f3945-2e7d-48fe-b83f-fdfd76ec6e54", 2, false, "tobias@gmail.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "b2b87818-8027-4da0-afe5-ad5712d0d65a", "04b60415-7244-4bd2-9870-7f1efca14e71" },
                    { "0a02ad2d-9ab6-45c9-938a-4607226f7342", "12f8e39b-32ad-408f-bda7-8c234fd54cf6" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "b2b87818-8027-4da0-afe5-ad5712d0d65a", "04b60415-7244-4bd2-9870-7f1efca14e71" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "0a02ad2d-9ab6-45c9-938a-4607226f7342", "12f8e39b-32ad-408f-bda7-8c234fd54cf6" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0a02ad2d-9ab6-45c9-938a-4607226f7342");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b2b87818-8027-4da0-afe5-ad5712d0d65a");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "04b60415-7244-4bd2-9870-7f1efca14e71");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "12f8e39b-32ad-408f-bda7-8c234fd54cf6");

            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "067028e6-3218-4632-9086-6e5a3eac11a3", null, "Admin", "ADMIN" },
                    { "0793c5be-2c8a-4aee-abe9-97127838911f", null, "Customer", "CUSTOMER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "AddressId", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "ShoppingCartId", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "43218869-d83d-4e9c-b19e-c1adc1da1453", 0, 1, "3d6ca0b4-edbc-42bd-a5eb-a01dd832c53c", "Customer", "admin@gmail.com", true, "Admin", "Adminsson", true, null, "ADMIN@GMAIL.COM", "ADMIN@GMAIL.COM", "AQAAAAIAAYagAAAAENuyISfxmJLMxvqV4oWfz/Hs1eJE7P0yGv54CmTSS30UgPpSUUWmsl5WqpysUn/fpw==", "1234567890", true, "5ba17ede-a0a4-4a0f-bd86-2ec376d8dddd", 1, false, "admin@gmail.com" },
                    { "fdd69357-45d0-4f57-a4ed-555d7e5df98b", 0, 2, "466aa377-7c7c-474e-b394-3f606f12b945", "Customer", "tobias@gmail.com", true, "Tobias", "Skog", true, null, "TOBIAS@GMAIL.COM", "TOBIAS@GMAIL.COM", "AQAAAAIAAYagAAAAELNbfVTKMC+jO+jAaptTrU4T8ueshg7WUx4zG4073CxpgZ+fZjiNccWN5s2H9KvZpA==", "1234567890", true, "8b374c31-14de-4361-8f2f-767d0e11f586", 2, false, "tobias@gmail.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "067028e6-3218-4632-9086-6e5a3eac11a3", "43218869-d83d-4e9c-b19e-c1adc1da1453" },
                    { "0793c5be-2c8a-4aee-abe9-97127838911f", "fdd69357-45d0-4f57-a4ed-555d7e5df98b" }
                });
        }
    }
}
