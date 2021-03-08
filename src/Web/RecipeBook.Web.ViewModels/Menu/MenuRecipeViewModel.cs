namespace RecipeBook.Web.ViewModels.Menu
{
    using AutoMapper;
    using RecipeBook.Data.Models;
    using RecipeBook.Services.Mapping;
    using System;
    using System.Linq;

    public class MenuRecipeViewModel : IMapFrom<Recipe>, IHaveCustomMappings
    {
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

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Recipe, MenuRecipeViewModel>()
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
