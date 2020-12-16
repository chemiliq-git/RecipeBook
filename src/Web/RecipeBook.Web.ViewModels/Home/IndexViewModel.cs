using System;
using System.Collections.Generic;
using System.Text;

namespace RecipeBook.Web.ViewModels.Home
{
    public class IndexViewModel
    {
        public IEnumerable<IndexRecipeTypeViewModel> RecipeTypes { get; set; }
    }
}
