using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HyperTamagotchi_API.Migrations
{
    /// <inheritdoc />
    public partial class mssqllocal_migration_978 : Migration
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
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ShoppingCarts",
                columns: table => new
                {
                    ShoppingCartId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingCarts", x => x.ShoppingCartId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    AddressId = table.Column<int>(type: "int", nullable: true),
                    ShoppingCartId = table.Column<int>(type: "int", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
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
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Address_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Address",
                        principalColumn: "AddressId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_ShoppingCarts_ShoppingCartId",
                        column: x => x.ShoppingCartId,
                        principalTable: "ShoppingCarts",
                        principalColumn: "ShoppingCartId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
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
                        name: "FK_Orders_AspNetUsers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "AspNetUsers",
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
                    ImagePath = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
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
                        name: "FK_ShoppingItems_AspNetUsers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "AspNetUsers",
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
                table: "Address",
                columns: new[] { "AddressId", "City", "StreetAddress", "ZipCode" },
                values: new object[,]
                {
                    { 1, "Kiruna", "Timmermansgatan 2A", "98137" },
                    { 2, "Örnsköldsvik", "Rundvägen 11D", "89144" }
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "067028e6-3218-4632-9086-6e5a3eac11a3", null, "Admin", "ADMIN" },
                    { "0793c5be-2c8a-4aee-abe9-97127838911f", null, "Customer", "CUSTOMER" }
                });

            migrationBuilder.InsertData(
                table: "ShoppingCarts",
                column: "ShoppingCartId",
                values: new object[]
                {
                    1,
                    2
                });

            migrationBuilder.InsertData(
                table: "ShoppingItems",
                columns: new[] { "ShoppingItemId", "CurrencyType", "Description", "Discount", "Discriminator", "ImagePath", "Name", "Price", "Quantity", "Stock" },
                values: new object[,]
                {
                    { 1, "SEK", "Nutritious food pack to keep your Tamagotchi healthy and happy.", 1f, "ShoppingItem", "Assets/ShoppingItem/Food_Pack.png", "Tamagotchi Food Pack", 75f, null, (byte)200 },
                    { 2, "SEK", "Portable water bottle to keep your Tamagotchi hydrated.", 1f, "ShoppingItem", "Assets/ShoppingItem/Water_Bottle.png", "Tamagotchi Water Bottle", 50f, null, (byte)150 },
                    { 3, "SEK", "Cozy bed for your Tamagotchi to sleep and rest comfortably.", 1f, "ShoppingItem", "Assets/ShoppingItem/Bed.png", "Tamagotchi Bed", 200f, null, (byte)100 },
                    { 4, "SEK", "Fun exercise wheel to keep your Tamagotchi active and fit.", 1f, "ShoppingItem", "Assets/ShoppingItem/Exercise_Wheel.png", "Tamagotchi Exercise Wheel", 150f, null, (byte)120 },
                    { 5, "SEK", "Essential cleaning kit to maintain your Tamagotchi's hygiene.", 1f, "ShoppingItem", "Assets/ShoppingItem/Cleaning_Kit.png", "Tamagotchi Cleaning Kit", 100f, null, (byte)80 },
                    { 6, "SEK", "A set of fun toys to entertain your Tamagotchi.", 1f, "ShoppingItem", "Assets/ShoppingItem/Toy_Set.png", "Tamagotchi Toy Set", 60f, null, (byte)170 },
                    { 7, "SEK", "Vitamins and supplements for your Tamagotchi's wellbeing.", 1f, "ShoppingItem", "Assets/ShoppingItem/Health_Supplement.png", "Tamagotchi Health Supplement", 90f, null, (byte)130 },
                    { 8, "SEK", "Convenient carrier for traveling with your Tamagotchi safely.", 1f, "ShoppingItem", "Assets/ShoppingItem/Travel_Carrier.png", "Tamagotchi Travel Carrier", 180f, null, (byte)90 },
                    { 9, "SEK", "Complete bath kit to keep your Tamagotchi clean and fresh.", 1f, "ShoppingItem", "Assets/ShoppingItem/Bath_Kit.png", "Tamagotchi Bath Kit", 85f, null, (byte)110 },
                    { 10, "SEK", "Essential first aid items for your Tamagotchi's minor injuries.", 1f, "ShoppingItem", "Assets/ShoppingItem/First_Aid_Kit.png", "Tamagotchi First Aid Kit", 120f, null, (byte)75 },
                    { 11, "SEK", "Comprehensive grooming kit for your Tamagotchi's fur and nails.", 1f, "ShoppingItem", "Assets/ShoppingItem/Grooming_Kit.png", "Tamagotchi Grooming Kit", 110f, null, (byte)95 },
                    { 12, "SEK", "Soft and warm blanket for your Tamagotchi to snuggle in.", 1f, "ShoppingItem", "Assets/ShoppingItem/Blanket.png", "Tamagotchi Blanket", 70f, null, (byte)140 },
                    { 13, "SEK", "Stylish feeding dish perfect for serving Tamagotchi meals.", 1f, "ShoppingItem", "Assets/ShoppingItem/Feeding_Dish.png", "Tamagotchi Feeding Dish", 45f, null, (byte)160 },
                    { 14, "SEK", "Adorable sun hat to protect your Tamagotchi from the sun.", 1f, "ShoppingItem", "Assets/ShoppingItem/Sun_Hat.png", "Tamagotchi Sun Hat", 55f, null, (byte)180 },
                    { 15, "SEK", "Personalized ID tag with your Tamagotchi’s name and info.", 1f, "ShoppingItem", "Assets/ShoppingItem/Id_Tag.png", "Tamagotchi ID Tag", 35f, null, (byte)200 }
                });

            migrationBuilder.InsertData(
                table: "ShoppingItems",
                columns: new[] { "ShoppingItemId", "CurrencyType", "CustomerId", "Description", "Discount", "Discriminator", "Experience", "ImagePath", "Mood", "Name", "Price", "Quantity", "Stock", "TamagotchiColor", "TamagotchiStage", "TamagotchiType" },
                values: new object[,]
                {
                    { 16, "SEK", null, "Meet Rocker Rick, the egg Tamagotchi.", 1f, "Tamagotchi", (byte)0, "Assets/Tamagotchi/Rocker/Rocker_Egg_Default.png", (byte)1, "Rocker Rick", 200f, null, (byte)100, (byte)1, (byte)1, (byte)1 },
                    { 17, "SEK", null, "Meet Rocker Rhonda, the child Tamagotchi.", 1f, "Tamagotchi", (byte)50, "Assets/Tamagotchi/Rocker/Rocker_Child_Blue.png", (byte)5, "Rocker Rhonda", 250f, null, (byte)100, (byte)4, (byte)2, (byte)1 },
                    { 18, "SEK", null, "Meet Rocker Rex, the adult Tamagotchi.", 1f, "Tamagotchi", (byte)100, "Assets/Tamagotchi/Rocker/Rocker_Adult_Red.png", (byte)1, "Rocker Rex", 300f, null, (byte)100, (byte)2, (byte)3, (byte)1 },
                    { 19, "SEK", null, "Meet Farmer Fred, the egg Tamagotchi.", 1f, "Tamagotchi", (byte)0, "Assets/Tamagotchi/Farmer/Farmer_Egg_Default.png", (byte)1, "Farmer Fred", 200f, null, (byte)100, (byte)1, (byte)1, (byte)2 },
                    { 20, "SEK", null, "Meet Farmer Fiona, the child Tamagotchi.", 1f, "Tamagotchi", (byte)50, "Assets/Tamagotchi/Farmer/Farmer_Child_Green.png", (byte)3, "Farmer Fiona", 250f, null, (byte)100, (byte)3, (byte)2, (byte)2 },
                    { 21, "SEK", null, "Meet Farmer Frank, the adult Tamagotchi.", 1f, "Tamagotchi", (byte)100, "Assets/Tamagotchi/Farmer/Farmer_Adult_Red.png", (byte)4, "Farmer Frank", 300f, null, (byte)100, (byte)2, (byte)3, (byte)2 },
                    { 22, "SEK", null, "Meet Developer Darin, the egg Tamagotchi.", 1f, "Tamagotchi", (byte)0, "Assets/Tamagotchi/Developer/Developer_Egg_Default.png", (byte)1, "Developer Darin", 200f, null, (byte)100, (byte)1, (byte)1, (byte)3 },
                    { 23, "SEK", null, "Meet Developer Daisy, the child Tamagotchi.", 1f, "Tamagotchi", (byte)50, "Assets/Tamagotchi/Developer/Developer_Child_Blue.png", (byte)2, "Developer Daisy", 250f, null, (byte)100, (byte)4, (byte)2, (byte)3 },
                    { 24, "SEK", null, "Meet Developer Dave, the adult Tamagotchi.", 1f, "Tamagotchi", (byte)100, "Assets/Tamagotchi/Developer/Developer_Adult_Green.png", (byte)1, "Developer Dave", 300f, null, (byte)100, (byte)3, (byte)3, (byte)3 },
                    { 25, "SEK", null, "Meet Athlete Alex, the egg Tamagotchi.", 1f, "Tamagotchi", (byte)0, "Assets/Tamagotchi/Athlete/Athlete_Egg_Default.png", (byte)1, "Athlete Alex", 200f, null, (byte)100, (byte)1, (byte)1, (byte)4 },
                    { 26, "SEK", null, "Meet Athlete Annie, the child Tamagotchi.", 1f, "Tamagotchi", (byte)50, "Assets/Tamagotchi/Athlete/Athlete_Child_Green.png", (byte)5, "Athlete Annie", 250f, null, (byte)100, (byte)3, (byte)2, (byte)4 },
                    { 27, "SEK", null, "Meet Athlete Arnold, the adult Tamagotchi.", 1f, "Tamagotchi", (byte)100, "Assets/Tamagotchi/Athlete/Athlete_Adult_Red.png", (byte)1, "Athlete Arnold", 300f, null, (byte)100, (byte)2, (byte)3, (byte)4 },
                    { 28, "SEK", null, "Meet Constructor Colin, the egg Tamagotchi.", 1f, "Tamagotchi", (byte)0, "Assets/Tamagotchi/Constructor/Constructor_Egg_Default.png", (byte)1, "Constructor Colin", 200f, null, (byte)100, (byte)1, (byte)1, (byte)5 },
                    { 29, "SEK", null, "Meet Constructor Cindy, the child Tamagotchi.", 1f, "Tamagotchi", (byte)50, "Assets/Tamagotchi/Constructor/Constructor_Child_Blue.png", (byte)2, "Constructor Cindy", 250f, null, (byte)100, (byte)4, (byte)2, (byte)5 },
                    { 30, "SEK", null, "Meet Constructor Carl, the adult Tamagotchi.", 1f, "Tamagotchi", (byte)100, "Assets/Tamagotchi/Constructor/Constructor_Adult_Green.png", (byte)1, "Constructor Carl", 300f, null, (byte)100, (byte)3, (byte)3, (byte)5 },
                    { 31, "SEK", null, "Meet Philosopher Phil, the egg Tamagotchi.", 1f, "Tamagotchi", (byte)0, "Assets/Tamagotchi/Philosopher/Philosopher_Egg_Default.png", (byte)1, "Philosopher Phil", 200f, null, (byte)100, (byte)1, (byte)1, (byte)6 },
                    { 32, "SEK", null, "Meet Philosopher Pippa, the child Tamagotchi.", 1f, "Tamagotchi", (byte)50, "Assets/Tamagotchi/Philosopher/Philosopher_Child_Red.png", (byte)3, "Philosopher Pippa", 250f, null, (byte)100, (byte)2, (byte)2, (byte)6 },
                    { 33, "SEK", null, "Meet Philosopher Pete, the adult Tamagotchi.", 1f, "Tamagotchi", (byte)100, "Assets/Tamagotchi/Philosopher/Philosopher_Adult_Blue.png", (byte)4, "Philosopher Pete", 300f, null, (byte)100, (byte)4, (byte)3, (byte)6 }
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

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_AddressId",
                table: "AspNetUsers",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ShoppingCartId",
                table: "AspNetUsers",
                column: "ShoppingCartId");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

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
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "ShoppingItemOrders");

            migrationBuilder.DropTable(
                name: "ShoppingItemShoppingCarts");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "ShoppingItems");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropTable(
                name: "ShoppingCarts");
        }
    }
}
