namespace RecipeBook.Web.ViewModels.Recipe
{
    using System.Collections.Generic;

    using RecipeBook.Web.ViewModels.Common;

    public class IndexViewModel
    {
        public IndexViewModel()
        {
            this.ResultItems = new List<IndexRecipeItemViewModel>();
        }

        public IEnumerable<IndexRecipeItemViewModel> ResultItems { get; set; }

        public SearchDataModel SearchData { get; set; }
    }
}
