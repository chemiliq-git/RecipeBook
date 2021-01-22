using System.Collections.Generic;
using System.Text;

namespace RecipeBook.Services.Data
{
    public interface IIngredientsSetItemService
    {
        IEnumerable<T> GetByIngredientId<T>(string input);
    }
}
