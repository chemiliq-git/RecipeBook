namespace RecipeBook.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using RecipeBook.Data.Models;

    public interface IIngredientsSetService
    {
        IEnumerable<T> GetAll<T>();

        IEnumerable<T> GetById<T>(string input);

        IEnumerable<T> GetByRecipeId<T>(string input);

        Task<bool> CreateAsync(IngredientsSet input);

        Task<bool> UpdateAsync(IngredientsSet input, List<IngredientsSetItemDataModel> inputItems);
    }
}
