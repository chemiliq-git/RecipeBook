namespace RecipeBook.Services.Data
{
    using System;
    using System.Collections.Generic;

    using RecipeBook.Data.Models;
    using RecipeBook.Services.Mapping;

    public class RecipeDataModel : IMapFrom<RecipeBook.Data.Models.Recipe>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Text { get; set; }

        public string ImagePath { get; set; }

        public string RecipeTypeId { get; set; }

        public ICollection<IngredientRecipeType> IngredientRecipeTypes { get; set; }

        public string IngredientSetId { get; set; }

        public DateTime LastCooked { get; set; }
    }
}

