namespace RecipeBook.Web.ViewModels.Home
{
    using System.Collections.Generic;

    public class SideBarFilterViewModel
    {
        public string SearchedText { get; set; }

        public IEnumerable<SearchRecipeTypeViewModel> RecipeTypes { get; set; }

        public IEnumerable<SearchIngredientTypeViewModel> IngredientTypes { get; set; }

        public SearchDataModeEnum Mode { get; set; }
    }
}