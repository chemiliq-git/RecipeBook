namespace RecipeBook.Data.Models
{
    using System;
    using System.Collections.Generic;
    using RecipeBook.Data.Common.Models;

    public class Recipe : BaseDeletableModel<string>
    {
        public Recipe()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Votes = new HashSet<Vote>();
        }

        public string Name { get; set; }

        public string Text { get; set; }

        public string ImagePath { get; set; }

        public string RecipeTypeId { get; set; }

        public virtual RecipeType RecipeType { get; set; }

        public string IngredientRecipeTypeId { get; set; }

        public string IngredientSetId { get; set; }

        public virtual IngredientsSet IngredientSet { get; set; }

        public virtual ICollection<Vote> Votes { get; set; }
        public DateTime LastCooked { get; set; }

        public bool IsInMenu { get; set; }
    }
}
