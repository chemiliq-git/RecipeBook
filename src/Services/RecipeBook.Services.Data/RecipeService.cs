namespace RecipeBook.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using RecipeBook.Data.Common.Repositories;
    using RecipeBook.Data.Models;
    using RecipeBook.Services.Mapping;

    public class RecipeService : IRecipeService
    {
        private readonly IDeletableEntityRepository<Recipe> recipeRepository;

        public RecipeService(IDeletableEntityRepository<Recipe> recipeRepository)
        {
            this.recipeRepository = recipeRepository;
        }

        public IEnumerable<T> GetAll<T>()
        {
            IQueryable<Recipe> query =
               this.recipeRepository.All().OrderBy(x => x.Name);

            return query.To<T>().ToList();
        }

        public IEnumerable<T> GetByName<T>(string input)
        {
            IQueryable<Recipe> query =
               this.recipeRepository.All()
                .Where(r => r.Name.Contains(input))
                .OrderBy(x => x.Name);

            var result = query.To<T>().ToList();

            return result;
        }

        public IEnumerable<T> GetByNamesList<T>(string inputList)
        {
            List<string> inputArray = new List<string>();
            var result = new List<T>();

            if (!string.IsNullOrEmpty(inputList))
            {
                inputArray = inputList.Split(new char[] { ',', ' ' }, System.StringSplitOptions.RemoveEmptyEntries).ToList();

                IQueryable<Recipe> query = this.recipeRepository.All();
                foreach (string input in inputArray)
                {
                    query = query
                    .Where(r => r.Name.Contains(input))
                    .OrderBy(r => r.Name);
                }

                result = query.To<T>().ToList();
            }

            return result;
        }

        public IEnumerable<T> GetByRecipeTypes<T>(string inputList)
        {
            List<string> inputArray = new List<string>();
            var result = new List<T>();

            if (!string.IsNullOrEmpty(inputList))
            {
                inputArray = inputList.Split(new char[] { ',', ' ' }, System.StringSplitOptions.RemoveEmptyEntries).ToList();

                IQueryable<Recipe> recipes = this.recipeRepository.All();
                recipes = from recipe in recipes
                         where inputArray.Any(input => recipe.RecipeTypeId.Equals(input))
                         select recipe;

                result = recipes.To<T>().ToList();
            }

            return result;
        }

        public IEnumerable<T> GetByIngredients<T>(string inputList)
        {
            List<string> inputArray = new List<string>();
            var result = new List<T>();

            if (!string.IsNullOrEmpty(inputList))
            {
                IQueryable<Recipe> recipes = this.recipeRepository.All();
                recipes = from recipe in recipes
                          where recipe.IngredientSet.IngredientSetItems.Any(ingrSetItem => inputList.Contains(ingrSetItem.IngredientID))
                          select recipe;

                result = recipes.To<T>().ToList();
            }

            return result;
        }
    }
}
