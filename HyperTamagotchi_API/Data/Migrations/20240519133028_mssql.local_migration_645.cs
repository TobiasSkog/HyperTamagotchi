using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HyperTamagotchi_API.Migrations
{
    /// <inheritdoc />
    public partial class mssqllocal_migration_645 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "2c624260-9450-4d5b-b054-bd0bc68ef92d", "22cfebc1-8d94-4a10-9b98-507770279c32" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "3912e03b-215f-4005-a958-1da4fcf4483a", "6fc86bac-8833-46fe-9400-b594bc9f6d3f" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c624260-9450-4d5b-b054-bd0bc68ef92d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3912e03b-215f-4005-a958-1da4fcf4483a");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "22cfebc1-8d94-4a10-9b98-507770279c32");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6fc86bac-8833-46fe-9400-b594bc9f6d3f");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "933b9343-1b47-41be-963b-fa91ece9b165", null, "Customer", "CUSTOMER" },
                    { "ca0afdb9-5a34-414f-b222-62222a2efa92", null, "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "AddressId", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "ShoppingCartId", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "5cc5ae32-47b2-407c-a746-ca2cf43e617b", 0, 1, "6f9b307c-4b52-4fd2-9fd0-8b2b7c1049b2", "Customer", "admin@gmail.com", true, "Admin", "Adminsson", true, null, "ADMIN@GMAIL.COM", "ADMIN@GMAIL.COM", "AQAAAAIAAYagAAAAEFoPvE47dhvMzAfmGwYpegGegdbJL4tmxqkFx+R2KQak66VZqgpwwrvG1C/FIoyM/Q==", "1234567890", true, "6b0086bc-df97-4fd4-a285-33959f7a8a55", 1, false, "admin@gmail.com" },
                    { "91949caf-a550-4a1b-ae17-b92a86aebdd2", 0, 2, "c7ce428d-08c7-4117-a881-79e97f40ef5c", "Customer", "tobias@gmail.com", true, "Tobias", "Skog", true, null, "TOBIAS@GMAIL.COM", "TOBIAS@GMAIL.COM", "AQAAAAIAAYagAAAAEGQ4qJficF9XIE0Vv6zEqiv0NbzzsjTGGOg7fA+fUVp7m0Jb9gzZff/vEEdRRnmflw==", "1234567890", true, "5b045c63-4233-44b3-bc4c-644b5a57a5b2", 2, false, "tobias@gmail.com" }
                });

            migrationBuilder.UpdateData(
                table: "ShoppingItems",
                keyColumn: "ShoppingItemId",
                keyValue: 1,
                column: "ImagePath",
                value: "Assets/ShoppingItem/FoodPack.png");

            migrationBuilder.UpdateData(
                table: "ShoppingItems",
                keyColumn: "ShoppingItemId",
                keyValue: 2,
                column: "ImagePath",
                value: "Assets/ShoppingItem/WaterBottle.png");

            migrationBuilder.UpdateData(
                table: "ShoppingItems",
                keyColumn: "ShoppingItemId",
                keyValue: 3,
                column: "ImagePath",
                value: "Assets/ShoppingItem/Bed.png");

            migrationBuilder.UpdateData(
                table: "ShoppingItems",
                keyColumn: "ShoppingItemId",
                keyValue: 4,
                column: "ImagePath",
                value: "Assets/ShoppingItem/ExerciseWheel.png");

            migrationBuilder.UpdateData(
                table: "ShoppingItems",
                keyColumn: "ShoppingItemId",
                keyValue: 5,
                column: "ImagePath",
                value: "Assets/ShoppingItem/CleaningKit.png");

            migrationBuilder.UpdateData(
                table: "ShoppingItems",
                keyColumn: "ShoppingItemId",
                keyValue: 6,
                column: "ImagePath",
                value: "Assets/ShoppingItem/ToySet.png");

            migrationBuilder.UpdateData(
                table: "ShoppingItems",
                keyColumn: "ShoppingItemId",
                keyValue: 7,
                column: "ImagePath",
                value: "Assets/ShoppingItem/HealthSupplement.png");

            migrationBuilder.UpdateData(
                table: "ShoppingItems",
                keyColumn: "ShoppingItemId",
                keyValue: 8,
                column: "ImagePath",
                value: "Assets/ShoppingItem/TravelCarrier.png");

            migrationBuilder.UpdateData(
                table: "ShoppingItems",
                keyColumn: "ShoppingItemId",
                keyValue: 9,
                column: "ImagePath",
                value: "Assets/ShoppingItem/BathKit.png");

            migrationBuilder.UpdateData(
                table: "ShoppingItems",
                keyColumn: "ShoppingItemId",
                keyValue: 10,
                column: "ImagePath",
                value: "Assets/ShoppingItem/FirstAidKit.png");

            migrationBuilder.UpdateData(
                table: "ShoppingItems",
                keyColumn: "ShoppingItemId",
                keyValue: 11,
                column: "ImagePath",
                value: "Assets/ShoppingItem/GroomingKit.png");

            migrationBuilder.UpdateData(
                table: "ShoppingItems",
                keyColumn: "ShoppingItemId",
                keyValue: 12,
                column: "ImagePath",
                value: "Assets/ShoppingItem/Blanket.png");

            migrationBuilder.UpdateData(
                table: "ShoppingItems",
                keyColumn: "ShoppingItemId",
                keyValue: 13,
                column: "ImagePath",
                value: "Assets/ShoppingItem/FeedingDish.png");

            migrationBuilder.UpdateData(
                table: "ShoppingItems",
                keyColumn: "ShoppingItemId",
                keyValue: 14,
                column: "ImagePath",
                value: "Assets/ShoppingItem/SunHat.png");

            migrationBuilder.UpdateData(
                table: "ShoppingItems",
                keyColumn: "ShoppingItemId",
                keyValue: 15,
                column: "ImagePath",
                value: "Assets/ShoppingItem/IdTag.png");

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "ca0afdb9-5a34-414f-b222-62222a2efa92", "5cc5ae32-47b2-407c-a746-ca2cf43e617b" },
                    { "933b9343-1b47-41be-963b-fa91ece9b165", "91949caf-a550-4a1b-ae17-b92a86aebdd2" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "ca0afdb9-5a34-414f-b222-62222a2efa92", "5cc5ae32-47b2-407c-a746-ca2cf43e617b" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "933b9343-1b47-41be-963b-fa91ece9b165", "91949caf-a550-4a1b-ae17-b92a86aebdd2" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "933b9343-1b47-41be-963b-fa91ece9b165");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ca0afdb9-5a34-414f-b222-62222a2efa92");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5cc5ae32-47b2-407c-a746-ca2cf43e617b");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "91949caf-a550-4a1b-ae17-b92a86aebdd2");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2c624260-9450-4d5b-b054-bd0bc68ef92d", null, "Customer", "CUSTOMER" },
                    { "3912e03b-215f-4005-a958-1da4fcf4483a", null, "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "AddressId", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "ShoppingCartId", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "22cfebc1-8d94-4a10-9b98-507770279c32", 0, 2, "7870a1da-d410-4d82-bab1-c9057194c4f9", "Customer", "tobias@gmail.com", true, "Tobias", "Skog", true, null, "TOBIAS@GMAIL.COM", "TOBIAS@GMAIL.COM", "AQAAAAIAAYagAAAAEHB/Di4M5U7pS3rzBi/ZLRr+0ignhdsGDMGKtBg6kKu+K3ftL0ntWrozll6OtYkufA==", "1234567890", true, "0aab90db-c2fb-4c60-906b-3ddc1b9b42d7", 2, false, "tobias@gmail.com" },
                    { "6fc86bac-8833-46fe-9400-b594bc9f6d3f", 0, 1, "7399d232-1252-4065-aff3-ccd38596fe64", "Customer", "admin@gmail.com", true, "Admin", "Adminsson", true, null, "ADMIN@GMAIL.COM", "ADMIN@GMAIL.COM", "AQAAAAIAAYagAAAAEEQN7oRGoEf2F2iV2BoTUoMBbs0JNIerdau0+L2YMCkZZjmjZ8Z8O2te5u1JVa5zkg==", "1234567890", true, "6f1c62c6-678b-4713-827a-2d75f19405b9", 1, false, "admin@gmail.com" }
                });

            migrationBuilder.UpdateData(
                table: "ShoppingItems",
                keyColumn: "ShoppingItemId",
                keyValue: 1,
                column: "ImagePath",
                value: "FoodPack.png");

            migrationBuilder.UpdateData(
                table: "ShoppingItems",
                keyColumn: "ShoppingItemId",
                keyValue: 2,
                column: "ImagePath",
                value: "WaterBottle.png");

            migrationBuilder.UpdateData(
                table: "ShoppingItems",
                keyColumn: "ShoppingItemId",
                keyValue: 3,
                column: "ImagePath",
                value: "Bed.png");

            migrationBuilder.UpdateData(
                table: "ShoppingItems",
                keyColumn: "ShoppingItemId",
                keyValue: 4,
                column: "ImagePath",
                value: "ExerciseWheel.png");

            migrationBuilder.UpdateData(
                table: "ShoppingItems",
                keyColumn: "ShoppingItemId",
                keyValue: 5,
                column: "ImagePath",
                value: "CleaningKit.png");

            migrationBuilder.UpdateData(
                table: "ShoppingItems",
                keyColumn: "ShoppingItemId",
                keyValue: 6,
                column: "ImagePath",
                value: "ToySet.png");

            migrationBuilder.UpdateData(
                table: "ShoppingItems",
                keyColumn: "ShoppingItemId",
                keyValue: 7,
                column: "ImagePath",
                value: "HealthSupplement.png");

            migrationBuilder.UpdateData(
                table: "ShoppingItems",
                keyColumn: "ShoppingItemId",
                keyValue: 8,
                column: "ImagePath",
                value: "TravelCarrier.png");

            migrationBuilder.UpdateData(
                table: "ShoppingItems",
                keyColumn: "ShoppingItemId",
                keyValue: 9,
                column: "ImagePath",
                value: "BathKit.png");

            migrationBuilder.UpdateData(
                table: "ShoppingItems",
                keyColumn: "ShoppingItemId",
                keyValue: 10,
                column: "ImagePath",
                value: "FirstAidKit.png");

            migrationBuilder.UpdateData(
                table: "ShoppingItems",
                keyColumn: "ShoppingItemId",
                keyValue: 11,
                column: "ImagePath",
                value: "GroomingKit.png");

            migrationBuilder.UpdateData(
                table: "ShoppingItems",
                keyColumn: "ShoppingItemId",
                keyValue: 12,
                column: "ImagePath",
                value: "Blanket.png");

            migrationBuilder.UpdateData(
                table: "ShoppingItems",
                keyColumn: "ShoppingItemId",
                keyValue: 13,
                column: "ImagePath",
                value: "FeedingDish.png");

            migrationBuilder.UpdateData(
                table: "ShoppingItems",
                keyColumn: "ShoppingItemId",
                keyValue: 14,
                column: "ImagePath",
                value: "SunHat.png");

            migrationBuilder.UpdateData(
                table: "ShoppingItems",
                keyColumn: "ShoppingItemId",
                keyValue: 15,
                column: "ImagePath",
                value: "IdTag.png");

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "2c624260-9450-4d5b-b054-bd0bc68ef92d", "22cfebc1-8d94-4a10-9b98-507770279c32" },
                    { "3912e03b-215f-4005-a958-1da4fcf4483a", "6fc86bac-8833-46fe-9400-b594bc9f6d3f" }
                });
        }
    }
}
