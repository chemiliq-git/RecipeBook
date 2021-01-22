namespace RecipeBook.Web.ViewModels.IngredientsSet
{
    using System.Collections.Generic;

    using RecipeBook.Web.ViewModels.Home;

    public class IngredientsSetScreenViewModel
    {
        public IngredientsSetScreenViewModel()
        {
            SearchResultItems = new List<SearchResultItemViewModel>();
            SearchData = new SearchDataModel();
            IngredientsSet = new IngredientsSetViewModel();
        }

        
        public IEnumerable<SearchResultItemViewModel> SearchResultItems { get; set; }

        public SearchDataModel SearchData { get; set; }

        public IngredientsSetViewModel IngredientsSet { get; set; }
    }
}
