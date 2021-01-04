namespace RecipeBook.Web.ViewModels.Home
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using RecipeBook.Data.Models;
    using RecipeBook.Services.Mapping;

    public class SearchResultItemViewModel : IMapFrom<Recipe>, IMapFrom<Ingredient>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Text { get; set; }

        public string ImagePath { get; set; }

        public string Type { get; set; }
    }
}
