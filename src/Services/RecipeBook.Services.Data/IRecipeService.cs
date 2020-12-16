namespace RecipeBook.Services.Data
{
    using System.Collections.Generic;
    using System.Text;

    public interface IRecipeService
    {
        IEnumerable<T> GetAll<T>();

        IEnumerable<T> GetByInput<T>(string input);

        IEnumerable<T> GetByInputList<T>(List<string> input);
    }
}
