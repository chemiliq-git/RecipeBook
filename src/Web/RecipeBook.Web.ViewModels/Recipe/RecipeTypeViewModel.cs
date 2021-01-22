namespace RecipeBook.Web.ViewModels.Recipe
{
    using RecipeBook.Data.Models;
    using RecipeBook.Services.Mapping;

    public class RecipeTypeViewModel : IMapFrom<RecipeType>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string ImagePath { get; set; }
    }
}
