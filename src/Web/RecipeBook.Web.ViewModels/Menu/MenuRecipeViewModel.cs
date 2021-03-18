namespace RecipeBook.Web.ViewModels.Menu
{
    using AutoMapper;
    using Microsoft.AspNetCore.Http;
    using RecipeBook.Data.Models;
    using RecipeBook.Services.Mapping;
    using System;
    using System.Linq;
    using System.Security.Claims;

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
            get { return this.LastCookedDays + this.TasteRate - this.EasyRate; }
        }

        public void CreateMappings(IProfileExpression configuration, IHttpContextAccessor httpContextAccessor)
        {
            configuration.CreateMap<Recipe, MenuRecipeViewModel>()
                .ForMember(vm => vm.TasteRate, options =>
                {
                    options.MapFrom(r => (r.Votes.Where(v => v.Type == VoteTypeEnm.Taste && v.UserId == httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value).ToList().Count > 0) ?
                    (int)r.Votes.Where(v => v.Type == VoteTypeEnm.Taste && v.UserId == httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value).First<Vote>().Value : 0);
                })
                .ForMember(vm => vm.EasyRate, options =>
                {
                    options.MapFrom(r => (r.Votes.Where(v => v.Type == VoteTypeEnm.Easy && v.UserId == httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value).ToList().Count > 0) ?
                    (int)r.Votes.Where(v => v.Type == VoteTypeEnm.Easy
                    && v.UserId == httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value).First<Vote>().Value : 0);
                })
                .ForMember(vm => vm.LastCookedDays, options =>
                {
                    options.MapFrom(r => (DateTime.Now - r.LastCooked).Days);
                });
        }
    }
}
