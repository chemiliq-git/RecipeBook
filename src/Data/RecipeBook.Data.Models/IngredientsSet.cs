namespace RecipeBook.Data.Models
{
    using System;
    using System.Collections.Generic;

    using RecipeBook.Data.Common.Models;

    public class IngredientsSet : BaseDeletableModel<string>
    {
        public IngredientsSet()
        {
            //TODO remove?
            this.Id = Guid.NewGuid().ToString();
            this.IngredientSetItems = new HashSet<IngredientsSetItem>();
        }

        public string Name { get; set; }

        public virtual ICollection<IngredientsSetItem> IngredientSetItems { get; set; }

        public string RecipeID { get; set; }

        public virtual Recipe Recipe { get; set; }        
    }
}
