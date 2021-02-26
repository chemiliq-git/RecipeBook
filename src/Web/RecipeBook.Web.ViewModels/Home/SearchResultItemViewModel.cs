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
        public SearchResultItemViewModel()
        {
            Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public string Text { get; set; }

        public string ImagePath { get; set; }

        public int TasteRate { get; set; }

        public int EasyRate { get; set; }

        public int LastCookedDays { get; set; }

        public decimal RecipeScore
        {
            get { return this.LastCookedDays + this.TasteRate + this.EasyRate; }
        }

        public string Type { get; set; }

        public bool IsInMenu { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Ingredient, SearchResultItemViewModel>()
                .ForMember(vm => vm.Type, options => options.MapFrom(i => "Ingredient"));

            configuration.CreateMap<Recipe, SearchResultItemViewModel>()
                .ForMember(vm => vm.Type, options => options.MapFrom(r => "Recipe"))
                .ForMember(vm => vm.TasteRate, options =>
                {
                    options.MapFrom(r => (r.Votes.Where(v => v.Type == VoteTypeEnm.Taste).ToList().Count > 0) ? (int)r.Votes.Where(v => v.Type == VoteTypeEnm.Taste).Average(v => v.Value) : 0);
                })
                .ForMember(vm => vm.EasyRate, options =>
                {
                    options.MapFrom(r => (r.Votes.Where(v => v.Type == VoteTypeEnm.Easy).ToList().Count > 0) ? (int)r.Votes.Where(v => v.Type == VoteTypeEnm.Easy).Average(v => v.Value) : 0);
                })
                .ForMember(vm => vm.LastCookedDays, options =>
                {
                    options.MapFrom(r => (DateTime.Now - r.LastCooked).Days);
                });
        }
    }
}
