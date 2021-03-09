namespace RecipeBook.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
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

        public T GetById<T>(string input)
        {
            IQueryable<Recipe> query =
               this.recipeRepository.All()
                .Where(r => r.Id.Equals(input));

            var result = query.To<T>().ToList();

            return result[0];
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

        public IEnumerable<T> GetByIsInMenu<T>()
        {
            List<string> inputArray = new List<string>();
            var result = new List<T>();

            var recipes = this.recipeRepository.All();
            recipes = from recipe in recipes
                          where recipe.IsInMenu == true
                          select recipe;

            result = recipes.To<T>().ToList();

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

        public async Task<bool> CreateAsync(RecipeDataModel inputRecipe)
        {
            try
            {
                Recipe recipe = new Recipe();
                recipe.Id = inputRecipe.Id;
                recipe.ImagePath = inputRecipe.ImagePath;
                recipe.Name = inputRecipe.Name;
                recipe.Text = inputRecipe.Text;
                recipe.RecipeTypeId = inputRecipe.RecipeTypeId;
                //recipe.IngredientRecipeTypeId = inputRecipe.;
                recipe.IngredientSetId = inputRecipe.IngredientSetId;
                recipe.LastCooked = inputRecipe.LastCooked;
                DateTime vNow = DateTime.UtcNow;
                recipe.CreatedOn = vNow;
                recipe.ModifiedOn = vNow;
                recipe.IsDeleted = false;
                await this.recipeRepository.AddAsync(recipe);
                await this.recipeRepository.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateAsync(RecipeDataModel inputRecipe)
        {
            try
            {
                var recipes = this.recipeRepository.All()
                .Where(r => r.Id == inputRecipe.Id)
                .ToList();

                if (recipes.Count > 0)
                {
                    recipes[0].Id = inputRecipe.Id;
                    recipes[0].ImagePath = inputRecipe.ImagePath;
                    recipes[0].Name = inputRecipe.Name;
                    recipes[0].Text = inputRecipe.Text;
                    recipes[0].RecipeTypeId = inputRecipe.RecipeTypeId;
                    recipes[0].IngredientSetId = inputRecipe.IngredientSetId;
                    recipes[0].PreparationTime = inputRecipe.PreparationTime;
                    recipes[0].LastCooked = inputRecipe.LastCooked;
                    DateTime vNow = DateTime.UtcNow;
                    recipes[0].ModifiedOn = vNow;
                    this.recipeRepository.Update(recipes[0]);
                    await this.recipeRepository.SaveChangesAsync();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteAsync(string inputId)
        {
            try
            {
                var recipes = this.recipeRepository.All()
                .Where(r => r.Id == inputId)
                .ToList();
                if (recipes.Count > 0)
                {
                    this.recipeRepository.Delete(recipes[0]);
                    await this.recipeRepository.SaveChangesAsync();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateLastCookedDate(string inputId, DateTime currentDateTime)
        {
            try
            {
                var recipes = this.recipeRepository.All()
                .Where(r => r.Id == inputId)
                .ToList();

                if (recipes.Count > 0)
                {
                    recipes[0].LastCooked = currentDateTime;
                    this.recipeRepository.Update(recipes[0]);
                    await this.recipeRepository.SaveChangesAsync();
                    return true;
                }

                return false;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> AddRecipeToMenu(string inputId)
        {
            try
            {
                var recipes = this.recipeRepository.All()
                .Where(r => r.Id == inputId)
                .ToList();

                if (recipes.Count > 0)
                {
                    recipes[0].IsInMenu = true;
                    this.recipeRepository.Update(recipes[0]);
                    await this.recipeRepository.SaveChangesAsync();
                    return true;
                }

                return false;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> RemoveRecipeFromMenu(string inputId)
        {
            try
            {
                var recipes = this.recipeRepository.All()
                .Where(r => r.Id == inputId)
                .ToList();

                if (recipes.Count > 0)
                {
                    recipes[0].IsInMenu = false;
                    this.recipeRepository.Update(recipes[0]);
                    await this.recipeRepository.SaveChangesAsync();
                    return true;
                }

                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}
