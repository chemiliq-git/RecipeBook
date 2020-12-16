using System.Collections.Generic;
using System.Text;

namespace RecipeBook.Services.Data
{
    public interface IIngredientTypeService
    {
        IEnumerable<T> GetAll<T>();

        IEnumerable<T> GetByInput<T>(string input);
    }
}
