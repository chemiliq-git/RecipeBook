namespace RecipeBook.Web.ViewModels.Home
{
    using System.Collections.Generic;

    using RecipeBook.Data.Models;
    using RecipeBook.Services.Mapping;

    public class SearchRecipeTypeViewModel : IMapFrom<RecipeType>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<SearchRecipeViewModel> Recipes { get; set; }
    }
}
