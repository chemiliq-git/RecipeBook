namespace RecipeBook.Services.Data
{
    using RecipeBook.Data.Models;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface IIngredientService
    {
        IEnumerable<T> GetAll<T>();

        IEnumerable<T> GetById<T>(string input);

        IEnumerable<T> GetByIdList<T>(string input);

        IEnumerable<T> GetByName<T>(string input);

        IEnumerable<T> GetByNamesList<T>(string input);

        Task<bool> CreateAsync(Ingredient input);

        Task<bool> UpdateAsync(Ingredient input);
    }
}
