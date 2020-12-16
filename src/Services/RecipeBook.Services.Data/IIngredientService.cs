namespace RecipeBook.Services.Data
{
    using System.Collections.Generic;
    using System.Text;

    public interface IIngredientService
    {
        IEnumerable<T> GetAll<T>();

        IEnumerable<T> GetByInput<T>(string input);

        IEnumerable<T> GetByInputList<T>(List<string> input);
    }
}
