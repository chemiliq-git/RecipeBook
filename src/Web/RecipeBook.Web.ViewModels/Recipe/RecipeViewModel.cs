namespace RecipeBook.Web.ViewModels.Recipe
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using RecipeBook.Data.Models;
    using RecipeBook.Services.Mapping;
    using RecipeBook.Web.ViewModels.IngredientsSet;

    public class RecipeViewModel : IMapFrom<RecipeBook.Data.Models.Recipe>, IHaveCustomMappings
    {
        public RecipeViewModel()
        {
            this.Id = Guid.NewGuid().ToString();            
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public string Text { get; set; }

        public string ImagePath { get; set; }

        public RecipeType RecipeType { get; set; }

        public ICollection<RecipeTypeViewModel> AllRecipeTypes { get; set; }

        public ICollection<IngredientRecipeType> IngredientRecipeTypes { get; set; }

        public IngredientsSetViewModel IngredientSet { get; set; }

        public ICollection<Vote> Votes { get; set; }

        public int TasteRate { get; set; }

        public DateTime LastCooked { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Recipe, RecipeViewModel>()
                .ForMember(vm => vm.TasteRate, options =>
                {
                    options.MapFrom(r => (r.Votes.Count > 0) ? (int)r.Votes.Average(v => v.Value) : 0);
                });
        }
    }
}
