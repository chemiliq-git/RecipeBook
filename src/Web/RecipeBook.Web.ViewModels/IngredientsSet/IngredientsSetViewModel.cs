namespace RecipeBook.Web.ViewModels.IngredientsSet
{
    using System;
    using System.Collections.Generic;

    using RecipeBook.Data.Models;
    using RecipeBook.Services.Mapping;

    public class IngredientsSetViewModel : IMapFrom<IngredientsSet>
    {
        public IngredientsSetViewModel()
        {
            this.Id = Guid.NewGuid().ToString();
            this.IngredientSetItems = new List<IngredientsSetItemViewModel>();
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public ICollection<IngredientsSetItemViewModel> IngredientSetItems { get; set; }
    }
}
