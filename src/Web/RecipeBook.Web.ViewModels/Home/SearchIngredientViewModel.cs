namespace RecipeBook.Web.ViewModels.Home
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using RecipeBook.Data.Models;
    using RecipeBook.Services.Mapping;

    public class SearchIngredientViewModel : IMapFrom<Ingredient>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public bool Checked { get; set; }
    }
}
