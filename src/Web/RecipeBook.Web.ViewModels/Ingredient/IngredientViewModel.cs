namespace RecipeBook.Web.ViewModels.Ingredeint
{
    using RecipeBook.Data.Models;
    using RecipeBook.Services.Mapping;
    using RecipeBook.Web.ViewModels.Ingredient;
    using System;
    using System.Collections.Generic;

    public class IngredientViewModel : IMapFrom<Ingredient>
    {
        public IngredientViewModel()
        {
            this.Id = Guid.NewGuid().ToString();
            this.AllIngredientType = new List<IngredientTypeViewModel>();
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public string ImagePath { get; set; }

        public string IngredientTypeID { get; set; }

        public IngredientType IngredientType { get; set; }

        public List<IngredientTypeViewModel> AllIngredientType { get; set; }
    }
}
