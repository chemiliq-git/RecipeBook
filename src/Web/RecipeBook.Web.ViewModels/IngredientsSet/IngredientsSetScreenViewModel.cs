namespace RecipeBook.Web.ViewModels.IngredientsSet
{
    using System.Collections.Generic;

    using RecipeBook.Web.ViewModels.Home;

    public class IngredientsSetScreenViewModel
    {
        public IngredientsSetScreenViewModel()
        {
            this.SearchResultItems = new List<SearchResultItemViewModel>();
            this.SearchData = new SearchDataModel();
            this.IngredientsSetItems = new List<IngredientsSetItemViewModel>();
        }

        public List<SearchResultItemViewModel> SearchResultItems { get; set; }

        public SearchDataModel SearchData { get; set; }

        public string IngredientsSetId { get; set; }

        public string IngredientsSetName { get; set; }

        public List<IngredientsSetItemViewModel> IngredientsSetItems { get; set; }
        
        public string RecipeId { get; set; }

        public string Mode { get; set; }
    }
}
