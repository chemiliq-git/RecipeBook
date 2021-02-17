namespace RecipeBook.Web.ViewModels.Ingredeint
{
    using RecipeBook.Data.Models;
    using RecipeBook.Services.Mapping;
    using RecipeBook.Web.ViewModels.Ingredient;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class IngredientViewModel : IMapFrom<Ingredient>
    {
        public IngredientViewModel()
        {
            this.Id = Guid.NewGuid().ToString();
            this.AllIngredientType = new List<IngredientTypeViewModel>();
        }

        public string Id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Name length can't be more than 100 symbols and less than 1 symbol.")]
        [RegularExpression(@"^.*[^\d\W]", ErrorMessage = "Name can't contain digits.")]
        public string Name { get; set; }

        public string ImagePath { get; set; }

        public string IngredientTypeID { get; set; }

        public IngredientType IngredientType { get; set; }

        public List<IngredientTypeViewModel> AllIngredientType { get; set; }
    }
}
