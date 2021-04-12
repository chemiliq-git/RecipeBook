namespace RecipeBook.Services.Data
{
    using RecipeBook.Data.Models;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface IRecipeService
    {
        IEnumerable<T> GetAll<T>();

        T GetById<T>(string input);

        IEnumerable<T> GetByNames<T>(string inputList);

        IEnumerable<T> GetByIsInMenu<T>();

        IEnumerable<T> GetFromHomeSearchData<T>(string text, string recipeTypes);

        IEnumerable<T> GetByNamesAndRecipeTypeIdsAndIngrIds<T>(string text, string recipeTypes, string ingredients);

        Task<bool> CreateAsync(RecipeDataModel input);

        Task<bool> UpdateAsync(RecipeDataModel input);

        Task<bool> DeleteAsync(string inputId);

        Task<bool> UpdateLastCookedDate(string inputId, DateTime currentDateTime);

        Task<bool> AddRecipeToMenu(string inputId);

        Task<bool> RemoveRecipeFromMenu(string inputId);
    }
}
