using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HyperTamagotchi_API.Data.Migrations
{
    /// <inheritdoc />
    public partial class wopsie : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ShoppingItemId",
                table: "OrderItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_ShoppingItemId",
                table: "OrderItems",
                column: "ShoppingItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_ShoppingItems_ShoppingItemId",
                table: "OrderItems",
                column: "ShoppingItemId",
                principalTable: "ShoppingItems",
                principalColumn: "ShoppingItemId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_ShoppingItems_ShoppingItemId",
                table: "OrderItems");

            migrationBuilder.DropIndex(
                name: "IX_OrderItems_ShoppingItemId",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "ShoppingItemId",
                table: "OrderItems");
        }
    }
}
