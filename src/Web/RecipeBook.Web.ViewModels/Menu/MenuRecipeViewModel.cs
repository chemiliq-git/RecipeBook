namespace RecipeBook.Web.ViewModels.Menu
{
    using RecipeBook.Data.Models;
    using RecipeBook.Services.Mapping;

    public class MenuRecipeViewModel : IMapFrom<Recipe>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Text { get; set; }

        public string ImagePath { get; set; }

        public int TasteRate { get; set; }

        public int EasyRate { get; set; }
    }
}
