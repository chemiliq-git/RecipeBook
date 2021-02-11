namespace RecipeBook.Web.ViewModels.Ingredeint
{
    using RecipeBook.Web.ViewModels.Home;
    using System.Collections.Generic;

    public class IndexViewModel
    {
        public IEnumerable<IngredientViewModel> ResultItems { get; set; }

        public SearchDataModel SearchData { get; set; }
    }
}
