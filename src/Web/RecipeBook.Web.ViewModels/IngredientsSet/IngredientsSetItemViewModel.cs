namespace RecipeBook.Web.ViewModels.IngredientsSet
{
    using RecipeBook.Data.Models;
    using RecipeBook.Services.Mapping;
    using System;

    public class IngredientsSetItemViewModel : IMapFrom<IngredientsSetItem>
    {
        public IngredientsSetItemViewModel()
        {
            Id = Guid.NewGuid().ToString();
        }
        public string Id { get; set; }
        public IngredientViewModel Ingredient { get; set; }

        public decimal QTY { get; set; }
    }
}
