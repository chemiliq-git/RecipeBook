namespace RecipeBook.Web.ViewModels.IngredientsSet
{
    using RecipeBook.Services.Mapping;

    public class IngredientViewModel : IMapFrom<RecipeBook.Data.Models.Ingredient>
    {
        public string Id { get; set; }

        public string Name { get; set; }
    }
}
