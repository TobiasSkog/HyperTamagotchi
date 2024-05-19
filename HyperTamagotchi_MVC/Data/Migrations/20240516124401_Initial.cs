using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HyperTamagotchi_MVC.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ShoppingItems",
                keyColumn: "ShoppingItemId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ShoppingItems",
                keyColumn: "ShoppingItemId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ShoppingItems",
                keyColumn: "ShoppingItemId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ShoppingItems",
                keyColumn: "ShoppingItemId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ShoppingItems",
                keyColumn: "ShoppingItemId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "ShoppingItems",
                keyColumn: "ShoppingItemId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "ShoppingItems",
                keyColumn: "ShoppingItemId",
                keyValue: 7);
        }
    }
}
