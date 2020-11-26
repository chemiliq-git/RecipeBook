namespace RecipeBook.Data.Models
{
    using RecipeBook.Data.Common.Models;
    using System;

    public class IngredientsSetItem : BaseDeletableModel<string>
    {
        public IngredientsSetItem()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string IngredientID { get; set; }

        public virtual Ingredient Ingredient { get; set; }

        public decimal QTY { get; set; }
    }
}
