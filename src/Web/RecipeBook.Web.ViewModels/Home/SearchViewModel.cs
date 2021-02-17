namespace RecipeBook.Web.ViewModels.Home
{
    using System.Collections.Generic;

    public class SearchViewModel
    {
        public SearchViewModel()
        {
            this.ResultItems = new List<SearchResultItemViewModel>();
        }

        public IEnumerable<SearchResultItemViewModel> ResultItems { get; set; }

        public SearchDataModel SearchData { get; set; }
    }
}
