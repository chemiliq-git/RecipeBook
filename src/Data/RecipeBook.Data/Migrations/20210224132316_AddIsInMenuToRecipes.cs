using Microsoft.EntityFrameworkCore.Migrations;

namespace RecipeBook.Data.Migrations
{
    public partial class AddIsInMenuToRecipes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsInMenu",
                table: "Recipes",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsInMenu",
                table: "Recipes");
        }
    }
}
