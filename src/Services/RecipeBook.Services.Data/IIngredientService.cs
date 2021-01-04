namespace RecipeBook.Services.Data
{
    using System.Collections.Generic;
    using System.Text;

    public interface IIngredientService
    {
        IEnumerable<T> GetAll<T>();

        IEnumerable<T> GetByName<T>(string input);

        IEnumerable<T> GetByNamesList<T>(string input);
    }
}
