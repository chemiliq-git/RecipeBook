namespace RecipeBook.Web.ViewModels.IngredientsSet
{
    using System.Collections.Generic;

    using RecipeBook.Web.ViewModels.Home;
    using RecipeBook.Web.ViewModels.Common;
    public class IngredientsSetScreenViewModel
    {
        public IngredientsSetScreenViewModel()
        {
            this.SearchResultItems = new List<IndexIngredientItemViewModel>();
            this.SearchData = new SearchDataModel();
            this.IngredientsSetItems = new List<IngredientsSetItemViewModel>();
        }

        public List<IndexIngredientItemViewModel> SearchResultItems { get; set; }

        public SearchDataModel SearchData { get; set; }

        public string IngredientsSetId { get; set; }

        public string IngredientsSetName { get; set; }

        public List<IngredientsSetItemViewModel> IngredientsSetItems { get; set; }
        
        public string RecipeId { get; set; }

        public string Mode { get; set; }
    }
}
