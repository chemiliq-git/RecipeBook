namespace RecipeBook.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using RecipeBook.Data.Models;

    internal class RecipeTypesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.RecipeTypes != null && dbContext.RecipeTypes.Any())
            {
                return;
            }

            var recipeTypes = new List<(string Name, string ImagePath)>
            {
                ("Desserts", @"D:\RecipeBook_Old\RecipeBook\wwwroot\images\Desserts.jpg"),
                ("Main dishes", @"D:\RecipeBook_Old\RecipeBook\wwwroot\images\main dishes.jpg"),
                ("Salads", @"D:\RecipeBook_Old\RecipeBook\wwwroot\images\Salads.jpg"),
                ("Soups",  @"D:\RecipeBook_Old\RecipeBook\wwwroot\images\Soups.jpg"),
            };
            foreach (var recipeType in recipeTypes)
            {
                await dbContext.RecipeTypes.AddAsync(new RecipeType
                {
                    Name = recipeType.Name,
                    ImagePath = recipeType.ImagePath,
                });
            }
        }
    }
}