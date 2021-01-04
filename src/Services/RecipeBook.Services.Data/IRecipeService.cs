namespace RecipeBook.Services.Data
{
    using System.Collections.Generic;
    using System.Text;

    public interface IRecipeService
    {
        IEnumerable<T> GetAll<T>();

        IEnumerable<T> GetByName<T>(string input);

        IEnumerable<T> GetByNamesList<T>(string inputList);

        IEnumerable<T> GetByRecipeTypes<T>(string inputList);

        IEnumerable<T> GetByIngredients<T>(string inputList);

    }
}
