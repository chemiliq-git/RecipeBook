using Microsoft.EntityFrameworkCore.Migrations;

namespace RecipeBook.Data.Migrations
{
    public partial class RecipeTasteAndPrepScaleIndexRemoved : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EasyScaleIndex",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "EasyScaleVotesNum",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "TasteScaleIndex",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "TasteScaleVotesNum",
                table: "Recipes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EasyScaleIndex",
                table: "Recipes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EasyScaleVotesNum",
                table: "Recipes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TasteScaleIndex",
                table: "Recipes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TasteScaleVotesNum",
                table: "Recipes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
