using Microsoft.EntityFrameworkCore.Migrations;

namespace RecipeBook.Data.Migrations
{
    public partial class IngredientsSetItemAddQTYDataRemoveQTY : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QTY",
                table: "IngredientsSetItems");

            migrationBuilder.RenameColumn(
                name: "IngredientSetID",
                table: "Recipes",
                newName: "IngredientSetId");

            migrationBuilder.AddColumn<string>(
                name: "QTYData",
                table: "IngredientsSetItems",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QTYData",
                table: "IngredientsSetItems");

            migrationBuilder.RenameColumn(
                name: "IngredientSetId",
                table: "Recipes",
                newName: "IngredientSetID");

            migrationBuilder.AddColumn<decimal>(
                name: "QTY",
                table: "IngredientsSetItems",
                type: "decimal(12,10)",
                precision: 12,
                scale: 10,
                nullable: false,
                defaultValue: 0m);
        }
    }
}
