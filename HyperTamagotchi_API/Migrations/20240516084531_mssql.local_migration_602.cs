using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HyperTamagotchi_API.Migrations
{
    /// <inheritdoc />
    public partial class mssqllocal_migration_602 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    AddressId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StreetAddress = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    City = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ZipCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.AddressId);
                });

            migrationBuilder.CreateTable(
                name: "ShoppingCarts",
                columns: table => new
                {
                    ShoppingCartId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingCarts", x => x.ShoppingCartId);
                });

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    AddressId = table.Column<int>(type: "int", nullable: false),
                    ShoppingCartId = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Customer_Address_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Address",
                        principalColumn: "AddressId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Customer_ShoppingCarts_ShoppingCartId",
                        column: x => x.ShoppingCartId,
                        principalTable: "ShoppingCarts",
                        principalColumn: "ShoppingCartId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ShippingDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExpectedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_Orders_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShoppingItems",
                columns: table => new
                {
                    ShoppingItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Stock = table.Column<byte>(type: "tinyint", nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false),
                    CurrencyType = table.Column<string>(type: "nvarchar(3)", nullable: false),
                    Discount = table.Column<float>(type: "real", nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Quantity = table.Column<byte>(type: "tinyint", nullable: true),
                    Discriminator = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    TamagotchiColor = table.Column<byte>(type: "tinyint", nullable: true),
                    TamagotchiType = table.Column<byte>(type: "tinyint", nullable: true),
                    Mood = table.Column<byte>(type: "tinyint", nullable: true),
                    TamagotchiStage = table.Column<byte>(type: "tinyint", nullable: true),
                    Experience = table.Column<byte>(type: "tinyint", nullable: true),
                    CustomerId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingItems", x => x.ShoppingItemId);
                    table.ForeignKey(
                        name: "FK_ShoppingItems_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ShoppingItemOrders",
                columns: table => new
                {
                    ShoppingItemId = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingItemOrders", x => new { x.ShoppingItemId, x.OrderId });
                    table.ForeignKey(
                        name: "FK_ShoppingItemOrders_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShoppingItemOrders_ShoppingItems_ShoppingItemId",
                        column: x => x.ShoppingItemId,
                        principalTable: "ShoppingItems",
                        principalColumn: "ShoppingItemId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShoppingItemShoppingCarts",
                columns: table => new
                {
                    ShoppingItemId = table.Column<int>(type: "int", nullable: false),
                    ShoppingCartId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingItemShoppingCarts", x => new { x.ShoppingItemId, x.ShoppingCartId });
                    table.ForeignKey(
                        name: "FK_ShoppingItemShoppingCarts_ShoppingCarts_ShoppingCartId",
                        column: x => x.ShoppingCartId,
                        principalTable: "ShoppingCarts",
                        principalColumn: "ShoppingCartId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShoppingItemShoppingCarts_ShoppingItems_ShoppingItemId",
                        column: x => x.ShoppingItemId,
                        principalTable: "ShoppingItems",
                        principalColumn: "ShoppingItemId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ShoppingItems",
                columns: new[] { "ShoppingItemId", "CurrencyType", "Description", "Discount", "Discriminator", "ImagePath", "Name", "Price", "Quantity", "Stock" },
                values: new object[,]
                {
                    { 1, "SEK", "Restores 10 energy to your Tamagotchi", 1f, "ShoppingItem", "none.png", "Banana", 25f, null, (byte)50 },
                    { 2, "SEK", "Restores 25 energy to your Tamagotchi", 1f, "ShoppingItem", "none.png", "Sports Drank", 50f, null, (byte)25 },
                    { 3, "SEK", "Restores 1 energy to your Tamagotchi", 1f, "ShoppingItem", "none.png", "Rice", 10f, null, (byte)250 }
                });

            migrationBuilder.InsertData(
                table: "ShoppingItems",
                columns: new[] { "ShoppingItemId", "CurrencyType", "CustomerId", "Description", "Discount", "Discriminator", "Experience", "ImagePath", "Mood", "Name", "Price", "Quantity", "Stock", "TamagotchiColor", "TamagotchiStage", "TamagotchiType" },
                values: new object[,]
                {
                    { 4, "SEK", null, "Meet the young developer Darin", 1f, "Tamagotchi", (byte)0, "Assets/Tamagotchi/Developer/Dev_Egg_Default.png", (byte)1, "Developer Darin", 200f, null, (byte)10, (byte)1, (byte)1, (byte)3 },
                    { 5, "SEK", null, "Meet the senior developer Juaaaahhhn", 1f, "Tamagotchi", (byte)50, "Assets/Tamagotchi/Developer/Dev_Child_Green.png", (byte)5, "Developer Juaaaahhhn", 255f, null, (byte)2, (byte)3, (byte)2, (byte)3 },
                    { 6, "SEK", null, "Meet the farmer Shaarraaa", 1f, "Tamagotchi", (byte)50, "Assets/Tamagotchi/Farmer/Farmer_Child_Blue.png", (byte)1, "Farmer Shaarraaa", 200f, null, (byte)6, (byte)4, (byte)2, (byte)2 },
                    { 7, "SEK", null, "Meet the farmer Ghäärryyy", 1f, "Tamagotchi", (byte)0, "Assets/Tamagotchi/Farmer/Farmer_Egg_Red.png", (byte)2, "Farmer Ghäärryyy", 50f, null, (byte)3, (byte)2, (byte)1, (byte)2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Customer_AddressId",
                table: "Customer",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_ShoppingCartId",
                table: "Customer",
                column: "ShoppingCartId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderDate",
                table: "Orders",
                column: "OrderDate");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingItemOrders_OrderId",
                table: "ShoppingItemOrders",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingItems_CustomerId",
                table: "ShoppingItems",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingItemShoppingCarts_ShoppingCartId",
                table: "ShoppingItemShoppingCarts",
                column: "ShoppingCartId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShoppingItemOrders");

            migrationBuilder.DropTable(
                name: "ShoppingItemShoppingCarts");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "ShoppingItems");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropTable(
                name: "ShoppingCarts");
        }
    }
}
