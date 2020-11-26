namespace RecipeBook.Data.Models
{
    using System;
    using System.Collections.Generic;

    using RecipeBook.Data.Common.Models;

    public class Ingredient : BaseDeletableModel<string>
    {
        public Ingredient()
        {
            this.Id = Guid.NewGuid().ToString();
            this.IngredientSetItems = new HashSet<IngredientsSetItem>();
        }

        public string Name { get; set; }

        public string ImagePath { get; set; }

        public string IngredientTypeID { get; set; }

        public virtual IngredientType IngredientType { get; set; }

        public virtual ICollection<IngredientsSetItem> IngredientSetItems { get; set; }
    }
}
