using Microsoft.EntityFrameworkCore.Migrations;

namespace RecipeBook.Data.Migrations
{
    public partial class VotesChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Votes_Recipes_RecipeId1",
                table: "Votes");

            migrationBuilder.DropIndex(
                name: "IX_Votes_RecipeId1",
                table: "Votes");

            migrationBuilder.DropColumn(
                name: "RecipeId1",
                table: "Votes");

            migrationBuilder.AlterColumn<string>(
                name: "RecipeId",
                table: "Votes",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Votes_RecipeId",
                table: "Votes",
                column: "RecipeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Votes_Recipes_RecipeId",
                table: "Votes",
                column: "RecipeId",
                principalTable: "Recipes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Votes_Recipes_RecipeId",
                table: "Votes");

            migrationBuilder.DropIndex(
                name: "IX_Votes_RecipeId",
                table: "Votes");

            migrationBuilder.AlterColumn<int>(
                name: "RecipeId",
                table: "Votes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RecipeId1",
                table: "Votes",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Votes_RecipeId1",
                table: "Votes",
                column: "RecipeId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Votes_Recipes_RecipeId1",
                table: "Votes",
                column: "RecipeId1",
                principalTable: "Recipes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
