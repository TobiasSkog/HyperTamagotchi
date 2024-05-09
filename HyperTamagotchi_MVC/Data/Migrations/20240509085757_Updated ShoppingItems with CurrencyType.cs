using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HyperTamagotchi_MVC.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedShoppingItemswithCurrencyType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CurrencyType",
                table: "ShoppingItems",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrencyType",
                table: "ShoppingItems");
        }
    }
}
