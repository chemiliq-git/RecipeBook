using Microsoft.EntityFrameworkCore.Migrations;

namespace RecipeBook.Data.Migrations
{
    public partial class AddIsMainItemToIngredientsSetItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IngredientsSetItems_IngredientsSets_IngredientsSetID",
                table: "IngredientsSetItems");

            migrationBuilder.RenameColumn(
                name: "IngredientsSetID",
                table: "IngredientsSetItems",
                newName: "IngredientsSetId");

            migrationBuilder.RenameIndex(
                name: "IX_IngredientsSetItems_IngredientsSetID",
                table: "IngredientsSetItems",
                newName: "IX_IngredientsSetItems_IngredientsSetId");

            migrationBuilder.AddForeignKey(
                name: "FK_IngredientsSetItems_IngredientsSets_IngredientsSetId",
                table: "IngredientsSetItems",
                column: "IngredientsSetId",
                principalTable: "IngredientsSets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IngredientsSetItems_IngredientsSets_IngredientsSetId",
                table: "IngredientsSetItems");

            migrationBuilder.RenameColumn(
                name: "IngredientsSetId",
                table: "IngredientsSetItems",
                newName: "IngredientsSetID");

            migrationBuilder.RenameIndex(
                name: "IX_IngredientsSetItems_IngredientsSetId",
                table: "IngredientsSetItems",
                newName: "IX_IngredientsSetItems_IngredientsSetID");

            migrationBuilder.AddForeignKey(
                name: "FK_IngredientsSetItems_IngredientsSets_IngredientsSetID",
                table: "IngredientsSetItems",
                column: "IngredientsSetID",
                principalTable: "IngredientsSets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
