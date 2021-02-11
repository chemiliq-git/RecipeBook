using System;
using System.Collections.Generic;
using System.Text;

namespace RecipeBook.Services.Data
{
    public class IngredientsSetItemDataModel
    {
        public string Id { get; set; }

        public string IngredientID { get; set; }

        public string QTYData { get; set; }

        public int Status { get; set; }

        public bool IsMainItem { get; set; }
    }
}
