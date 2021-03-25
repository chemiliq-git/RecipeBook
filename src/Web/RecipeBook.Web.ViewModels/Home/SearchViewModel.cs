namespace RecipeBook.Web.ViewModels.Home
{
    using System.Collections.Generic;

    public class SearchViewModel
    {
        public SearchViewModel()
        {
            this.ResultItems = new List<RecipesSearchResultItemViewModel>();
        }

        public IEnumerable<RecipesSearchResultItemViewModel> ResultItems { get; set; }

        public SearchDataModel SearchData { get; set; }
    }
}
