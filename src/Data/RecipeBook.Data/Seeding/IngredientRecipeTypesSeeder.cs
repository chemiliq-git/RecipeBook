namespace RecipeBook.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using RecipeBook.Data.Models;

    internal class IngredientRecipeTypesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.IngredientRecipeTypes != null && dbContext.IngredientRecipeTypes.Any())
            {
                return;
            }

            var ingrRrecipeTypes = new List<(string Name, string ImagePath)>
            {
                ("Meat", string.Empty),
                ("Beans", string.Empty),
                ("Dairy", string.Empty),
                ("Fish", string.Empty),
                ("Pasta&Dough", string.Empty),
            };
            foreach (var ingrRecipeType in ingrRrecipeTypes)
            {
                await dbContext.IngredientRecipeTypes.AddAsync(new IngredientRecipeType
                {
                    Name = ingrRecipeType.Name,
                    ImagePath = ingrRecipeType.ImagePath,
                });
            }
        }
    }
}