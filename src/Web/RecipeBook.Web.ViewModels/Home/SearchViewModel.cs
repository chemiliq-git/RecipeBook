namespace RecipeBook.Web.ViewModels.Home
{
    using System.Collections.Generic;

    public class SearchViewModel
    {
        public IEnumerable<SearchItemViewModel> ResultItems { get; set; }

        public IEnumerable<SearchRecipeTypeViewModel> RecipeTypes { get; set; }

        public IEnumerable<SearchIngredientTypeViewModel> IngredientTypes { get; set; }

    }
}
