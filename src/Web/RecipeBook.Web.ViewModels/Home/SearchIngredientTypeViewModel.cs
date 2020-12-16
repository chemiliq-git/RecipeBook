namespace RecipeBook.Web.ViewModels.Home
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using RecipeBook.Data.Models;
    using RecipeBook.Services.Mapping;

    public class SearchIngredientTypeViewModel : IMapFrom<IngredientType>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<SearchIngredientViewModel> Ingredients { get; set; }
    }
}
