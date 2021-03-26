namespace RecipeBook.Web.ViewModels.Ingredeint
{
    using System.Collections.Generic;

    using RecipeBook.Web.ViewModels.Common;

    public class IndexViewModel
    {
        public IEnumerable<IngredientViewModel> ResultItems { get; set; }

        public SearchDataModel SearchData { get; set; }
    }
}
