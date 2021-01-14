namespace RecipeBook.Web.ViewModels.Recipe
{
    using System;
    using System.Collections.Generic;

    using RecipeBook.Data.Models;
    using RecipeBook.Services.Mapping;

    public class RecipeViewModel : IMapFrom<RecipeBook.Data.Models.Recipe>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Text { get; set; }

        public string ImagePath { get; set; }

        public RecipeType RecipeType { get; set; }

        public ICollection<IngredientRecipeType> IngredientRecipeTypes { get; set; }

        public IngredientsSet IngredientSet { get; set; }

        public ICollection<Vote> Votes { get; set; }

        public DateTime LastCooked { get; set; }
    }
}
