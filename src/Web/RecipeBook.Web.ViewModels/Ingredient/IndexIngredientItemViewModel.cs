namespace RecipeBook.Web.ViewModels.Home
{
    using System;

    using RecipeBook.Data.Models;
    using RecipeBook.Services.Mapping;

    public class IndexIngredientItemViewModel : IMapFrom<Ingredient>, IMapTo<Ingredient>
    {
        public IndexIngredientItemViewModel()
        {
            Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public string Text { get; set; }

        public string ImagePath { get; set; }
    }
}
