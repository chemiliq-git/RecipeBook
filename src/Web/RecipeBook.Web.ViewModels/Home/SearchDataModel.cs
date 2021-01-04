namespace RecipeBook.Web.ViewModels.Home
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class SearchDataModel
    {
        public string Text { get; set; }

        public string RecipeTypes { get; set; }

        public string Ingredients { get; set; }

        public SearchDataModeEnum Mode { get; set; }
    }
}
