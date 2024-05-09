using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HyperTamagotchi_MVC.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedShoppingItemswithCurrencyTypeagain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CurrencyType",
                table: "ShoppingItems",
                type: "nvarchar(3)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "CurrencyType",
                table: "ShoppingItems",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(3)");
        }
    }
}
