namespace RecipeBook.Web.ViewModels.Home
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class IndexViewModel
    {
        public IEnumerable<IndexRecipeTypeViewModel> RecipeTypes { get; set; }

        public SearchDataModel SearchData { get; set; }
    }
}
