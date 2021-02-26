namespace RecipeBook.Web.ViewModels.Menu
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using RecipeBook.Services.Mapping;

    public class MenuViewModel
    {
        public List<MenuRecipeViewModel> AllItems { get; set; }
    }
}
