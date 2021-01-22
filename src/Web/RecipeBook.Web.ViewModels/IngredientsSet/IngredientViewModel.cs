using RecipeBook.Data.Models;
using RecipeBook.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecipeBook.Web.ViewModels.IngredientsSet
{
    public  class IngredientViewModel : IMapFrom<Ingredient>
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
