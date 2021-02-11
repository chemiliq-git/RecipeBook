namespace RecipeBook.Web.ViewModels.Ingredient
{
    using RecipeBook.Data.Models;
    using RecipeBook.Services.Mapping;

    public class IngredientTypeViewModel : IMapFrom<IngredientType>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string ImagePath { get; set; }
    }
}
