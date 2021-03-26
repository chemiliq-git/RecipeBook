namespace RecipeBook.Web.ViewModels.Ingredient
{
    using RecipeBook.Web.ViewModels.Ingredeint;
    using RecipeBook.Web.ViewModels.Common;

    public class DeleteIngredientViewModel
    {
        public IngredientViewModel SelectedItem { get; set; }

        public SearchDataModel SearchData { get; set; }
    }
}
