namespace RecipeBook.Data.Models
{
    using System;

    using RecipeBook.Data.Common.Models;

    public class IngredientsSetItem : BaseDeletableModel<string>
    {
        public IngredientsSetItem()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string IngredientID { get; set; }

        public virtual Ingredient Ingredient { get; set; }

        public string IngredientsSetId { get; set; }

        public string QTYData { get; set; }

        public bool IsMainItem { get; set; }
    }
}
