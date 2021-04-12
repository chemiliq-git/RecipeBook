namespace RecipeBook.Web.ViewModels.Recipe
{
    using System;
    using System.Linq;
    using System.Security.Claims;

    using AutoMapper;
    using Microsoft.AspNetCore.Http;
    using RecipeBook.Data.Models;
    using RecipeBook.Services.Mapping;

    public class IndexRecipeItemViewModel : IMapFrom<Recipe>, IMapTo<Recipe>, IHaveCustomMappings
    {
        public IndexRecipeItemViewModel()
        {
            this.Id = Guid.NewGuid().ToString();
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
            get { return this.LastCookedDays + this.TasteRate - this.EasyRate; }
        }

        public bool IsInMenu { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Recipe, IndexRecipeItemViewModel>()
                .ForMember(vm => vm.LastCookedDays, options =>
                {
                    options.MapFrom(r => (DateTime.Now - r.LastCooked).Days);
                });
        }
    }
}

