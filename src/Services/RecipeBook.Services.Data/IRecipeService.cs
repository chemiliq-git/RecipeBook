﻿namespace RecipeBook.Services.Data
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

        IEnumerable<T> GetByIsInMenu<T>();

        Task<bool> CreateAsync(RecipeDataModel input);

        Task<bool> UpdateAsync(RecipeDataModel input);

        Task<bool> DeleteAsync(string inputId);

        Task<bool> UpdateLastCookedDate(string inputId, DateTime currentDateTime);

        Task<bool> AddRecipeToMenu(string inputId);

        Task<bool> RemoveRecipeFromMenu(string inputId);
    }
}
