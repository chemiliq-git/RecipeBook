namespace RecipeBook.Web.ViewModels.Home
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using AutoMapper;
    using RecipeBook.Data.Models;
    using RecipeBook.Services.Mapping;

    public class SearchResultItemViewModel : IMapFrom<Recipe>, IMapFrom<Ingredient>, IMapTo<Recipe>, IMapTo<Ingredient>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Text { get; set; }

        public string ImagePath { get; set; }

        public int TasteRate { get; set; }

        public string Type { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Ingredient, SearchResultItemViewModel>()
                .ForMember(vm => vm.Type, options => options.MapFrom(i => "Ingredient"));

            configuration.CreateMap<Recipe, SearchResultItemViewModel>()
                .ForMember(vm => vm.Type, options => options.MapFrom(r => "Recipe"))
                .ForMember(vm => vm.TasteRate, options =>
                {
                    options.MapFrom(r => (r.Votes.Count > 0) ? (int)r.Votes.Average(v => v.Value) : 0);
                });
        }
    }
}
