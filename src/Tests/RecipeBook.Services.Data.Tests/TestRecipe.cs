using RecipeBook.Data.Models;
using RecipeBook.Services.Mapping;

namespace RecipeBook.Services.Data.Tests
{
    public class TestRecipe : IMapFrom<RecipeBook.Data.Models.Recipe>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Text { get; set; }

        public string ImagePath { get; set; }

        public RecipeType RecipeType { get; set; }
    }
}