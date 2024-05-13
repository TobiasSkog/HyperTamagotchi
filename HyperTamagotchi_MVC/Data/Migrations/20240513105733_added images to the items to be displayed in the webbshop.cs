using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HyperTamagotchi_MVC.Data.Migrations
{
    /// <inheritdoc />
    public partial class addedimagestotheitemstobedisplayedinthewebbshop : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "ShoppingItems",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "ShoppingItems");
        }
    }
}
