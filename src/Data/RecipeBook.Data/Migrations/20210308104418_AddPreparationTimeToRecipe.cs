using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RecipeBook.Data.Migrations
{
    public partial class AddPreparationTimeToRecipe : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<TimeSpan>(
                name: "PreparationTime",
                table: "Recipes",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PreparationTime",
                table: "Recipes");
        }
    }
}
