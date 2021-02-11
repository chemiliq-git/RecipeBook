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

        public string IngredientId { get; set; }

        public string IngredientName { get; set; }

        public string QTYData { get; set; }

        public int Status { get; set; }

        public bool IsMainItem { get; set; }

    }
}
