using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RecipeBook.Data.Migrations
{
    public partial class IngredientRecipeTypesAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IngredientSetItems");

            migrationBuilder.DropTable(
                name: "IngredientSets");

            migrationBuilder.AddColumn<string>(
                name: "IngredientRecipeTypeId",
                table: "Recipes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "IngredientRecipeTypes",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngredientRecipeTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IngredientsSets",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecipeID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngredientsSets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IngredientsSets_Recipes_RecipeID",
                        column: x => x.RecipeID,
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "IngredientRecipeTypeRecipe",
                columns: table => new
                {
                    IngredientRecipeTypesId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RecipesId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngredientRecipeTypeRecipe", x => new { x.IngredientRecipeTypesId, x.RecipesId });
                    table.ForeignKey(
                        name: "FK_IngredientRecipeTypeRecipe_IngredientRecipeTypes_IngredientRecipeTypesId",
                        column: x => x.IngredientRecipeTypesId,
                        principalTable: "IngredientRecipeTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IngredientRecipeTypeRecipe_Recipes_RecipesId",
                        column: x => x.RecipesId,
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "IngredientsSetItems",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IngredientID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    QTY = table.Column<decimal>(type: "decimal(12,10)", precision: 12, scale: 10, nullable: false),
                    IngredientsSetId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngredientsSetItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IngredientsSetItems_Ingredients_IngredientID",
                        column: x => x.IngredientID,
                        principalTable: "Ingredients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IngredientsSetItems_IngredientsSets_IngredientsSetId",
                        column: x => x.IngredientsSetId,
                        principalTable: "IngredientsSets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IngredientRecipeTypeRecipe_RecipesId",
                table: "IngredientRecipeTypeRecipe",
                column: "RecipesId");

            migrationBuilder.CreateIndex(
                name: "IX_IngredientRecipeTypes_IsDeleted",
                table: "IngredientRecipeTypes",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_IngredientsSetItems_IngredientID",
                table: "IngredientsSetItems",
                column: "IngredientID");

            migrationBuilder.CreateIndex(
                name: "IX_IngredientsSetItems_IngredientsSetId",
                table: "IngredientsSetItems",
                column: "IngredientsSetId");

            migrationBuilder.CreateIndex(
                name: "IX_IngredientsSetItems_IsDeleted",
                table: "IngredientsSetItems",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_IngredientsSets_IsDeleted",
                table: "IngredientsSets",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_IngredientsSets_RecipeID",
                table: "IngredientsSets",
                column: "RecipeID",
                unique: true,
                filter: "[RecipeID] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IngredientRecipeTypeRecipe");

            migrationBuilder.DropTable(
                name: "IngredientsSetItems");

            migrationBuilder.DropTable(
                name: "IngredientRecipeTypes");

            migrationBuilder.DropTable(
                name: "IngredientsSets");

            migrationBuilder.DropColumn(
                name: "IngredientRecipeTypeId",
                table: "Recipes");

            migrationBuilder.CreateTable(
                name: "IngredientSets",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecipeID = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngredientSets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IngredientSets_Recipes_RecipeID",
                        column: x => x.RecipeID,
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "IngredientSetItems",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IngredientID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    IngredientSetId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    QTY = table.Column<decimal>(type: "decimal(12,10)", precision: 12, scale: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngredientSetItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IngredientSetItems_Ingredients_IngredientID",
                        column: x => x.IngredientID,
                        principalTable: "Ingredients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IngredientSetItems_IngredientSets_IngredientSetId",
                        column: x => x.IngredientSetId,
                        principalTable: "IngredientSets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IngredientSetItems_IngredientID",
                table: "IngredientSetItems",
                column: "IngredientID");

            migrationBuilder.CreateIndex(
                name: "IX_IngredientSetItems_IngredientSetId",
                table: "IngredientSetItems",
                column: "IngredientSetId");

            migrationBuilder.CreateIndex(
                name: "IX_IngredientSetItems_IsDeleted",
                table: "IngredientSetItems",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_IngredientSets_IsDeleted",
                table: "IngredientSets",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_IngredientSets_RecipeID",
                table: "IngredientSets",
                column: "RecipeID",
                unique: true,
                filter: "[RecipeID] IS NOT NULL");
        }
    }
}
