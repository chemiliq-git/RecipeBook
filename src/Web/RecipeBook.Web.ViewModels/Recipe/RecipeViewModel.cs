namespace RecipeBook.Web.ViewModels.Recipe
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Security.Claims;
    using AutoMapper;
    using Microsoft.AspNetCore.Http;
    using RecipeBook.Data.Models;
    using RecipeBook.Services.Mapping;
    using RecipeBook.Web.ViewModels.IngredientsSet;

    public class RecipeViewModel : IMapFrom<RecipeBook.Data.Models.Recipe>
    {
        public RecipeViewModel()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Name length can't be more than 100 symbols and less than 1 symbol.")]
        public string Name { get; set; }

        public string Text { get; set; }

        public string ImagePath { get; set; }

        public RecipeType RecipeType { get; set; }

        public ICollection<RecipeTypeViewModel> AllRecipeTypes { get; set; }

        public ICollection<IngredientRecipeType> IngredientRecipeTypes { get; set; }

        public IngredientsSetViewModel IngredientSet { get; set; }

        public int TasteRate { get; set; }

        public int EasyRate { get; set; }

        public DateTime LastCooked { get; set; }

        public TimeSpan PreparationTime { get; set; }
    }
}
