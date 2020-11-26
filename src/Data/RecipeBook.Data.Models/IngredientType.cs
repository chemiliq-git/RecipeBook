namespace RecipeBook.Data.Models
{
    using System;
    using System.Collections.Generic;

    using RecipeBook.Data.Common.Models;

    public class IngredientType : BaseDeletableModel<string>
    {
        public IngredientType()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Ingredients = new HashSet<Ingredient>();
        }

        public string Name { get; set; }

        public string ImagePath { get; set; }

        public virtual ICollection<Ingredient> Ingredients { get; set; }
    }
}
