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

        IEnumerable<T> GetByName<T>(string input);

        IEnumerable<T> GetByNamesList<T>(string inputList);

        IEnumerable<T> GetByRecipeTypes<T>(string inputList);

        IEnumerable<T> GetByIngredients<T>(string inputList);

        Task<bool> CreateAsync(RecipeDataModel input);

        Task<bool> UpdateAsync(RecipeDataModel input);

        Task<bool> UpdateLastCookedDate(string inputId, DateTime currentDateTime);
    }
}
