namespace RecipeBook.Services.Data
{
    using RecipeBook.Data.Models;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface IRecipeService
    {
        IEnumerable<T> GetAll<T>();

        T GetById<T>(string input);

        IEnumerable<T> GetByName<T>(string input);

        IEnumerable<T> GetByNamesList<T>(string inputList);

        IEnumerable<T> GetByRecipeTypes<T>(string inputList);

        IEnumerable<T> GetByIngredients<T>(string inputList);

        Task<bool> CreateAsync(CreateRecipeDataModel input);

    }
}
