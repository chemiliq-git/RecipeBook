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
            this.IngredientRecipeTypes = new HashSet<IngredientRecipeType>();
        }

        public string Name { get; set; }

        public string Text { get; set; }

        public string ImagePath { get; set; }

        public string RecipeTypeId { get; set; }

        public virtual RecipeType RecipeType { get; set; }

        public string IngredientRecipeTypeId { get; set; }

        public virtual ICollection<IngredientRecipeType> IngredientRecipeTypes { get; set; }

        public string IngredientSetID { get; set; }

        public virtual IngredientsSet IngredientSet { get; set; }

        public int EasyScaleIndex { get; set; }

        public int EasyScaleVotesNum { get; set; }

        public int TasteScaleIndex { get; set; }

        public int TasteScaleVotesNum { get; set; }

        public DateTime LastCooked { get; set; }
    }
}
