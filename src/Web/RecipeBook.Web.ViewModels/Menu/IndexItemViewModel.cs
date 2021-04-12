namespace RecipeBook.Web.ViewModels.Menu
{
    using System;
    using System.Linq;
    using System.Security.Claims;

    using AutoMapper;
    using Microsoft.AspNetCore.Http;
    using RecipeBook.Data.Models;
    using RecipeBook.Services.Mapping;

    public class IndexItemViewModel : IMapFrom<Recipe>, IHaveCustomMappings
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
            get { return this.LastCookedDays + this.TasteRate - this.EasyRate; }
        }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Recipe, IndexItemViewModel>()
                .ForMember(vm => vm.LastCookedDays, options =>
                {
                    options.MapFrom(r => (DateTime.Now - r.LastCooked).Days);
                });
        }
    }
}
