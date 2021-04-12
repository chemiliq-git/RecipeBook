namespace RecipeBook.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using RecipeBook.Data.Common.Repositories;
    using RecipeBook.Data.Models;
    using RecipeBook.Services.Mapping;

    public class IngredientService : IIngredientService
    {
        private readonly IDeletableEntityRepository<Ingredient> ingredientRepository;

        public IngredientService(IDeletableEntityRepository<Ingredient> ingredientRepository)
        {
            this.ingredientRepository = ingredientRepository;
        }

        public IEnumerable<T> GetAll<T>()
        {
            IQueryable<Ingredient> query =
               this.ingredientRepository.All().OrderBy(x => x.Name);

            return query.To<T>().ToList();
        }

        public IEnumerable<T> GetById<T>(string input)
        {
            IQueryable<Ingredient> query =
               this.ingredientRepository.All()
                .Where(r => r.Id.Equals(input))
                .OrderBy(x => x.Name);
            return query.To<T>().ToList();
        }

        public IEnumerable<T> GetByName<T>(string input)
        {
            IQueryable<Ingredient> query =
               this.ingredientRepository.All()
                .Where(r => r.Name.Contains(input))
                .OrderBy(x => x.Name);
            return query.To<T>().ToList();
        }

        public IEnumerable<T> GetByNames<T>(string inputList)
        {
            List<string> inputArray = new List<string>();
            var result = new List<T>();

            if (!string.IsNullOrEmpty(inputList))
            {
                //inputArray = inputList.Split(new char[] { ',', ' ' }, System.StringSplitOptions.RemoveEmptyEntries).ToList();

                //IQueryable<Ingredient> ingredients = this.ingredientRepository.All();

                //ingredients = from ingr in ingredients
                //              where inputArray.Any(input => ingr.Name.Contains(input))
                //              select ingr;

                //result = ingredients.To<T>().ToList();


                IQueryable<Ingredient> query= this.GetByNamesQueryable(inputList);

                result = query.To<T>().ToList();
            }

            return result;
        }

        public IEnumerable<T> GetByIds<T>(string inputList)
        {
            List<string> inputArray = new List<string>();
            var result = new List<T>();

            if (!string.IsNullOrEmpty(inputList))
            {
                IQueryable<Ingredient> ingredients = this.GetByIdsQueryable(inputList);

                result = ingredients.To<T>().ToList();
            }

            return result;
        }

        public async Task<bool> CreateAsync(Ingredient ingredient)
        {
            try
            {
                DateTime dateTimeNow = DateTime.UtcNow;
                await this.ingredientRepository.AddAsync(ingredient);
                await this.ingredientRepository.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateAsync(Ingredient ingredient)
        {
            try
            {
                this.ingredientRepository.Update(ingredient);
                await this.ingredientRepository.SaveChangesAsync();
                return true;
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
                var ingredients = this.ingredientRepository.All()
                .Where(r => r.Id == inputId)
                .ToList();

                if (ingredients.Count > 0)
                {
                    this.ingredientRepository.Delete(ingredients[0]);
                    await this.ingredientRepository.SaveChangesAsync();
                    return true;
                }

                return false;
            }
            catch
            {
                return false;
            }
        }

        public IEnumerable<T> GetByNamesAndIds<T>(string text, string ingredients)
        {
            IQueryable<Ingredient> varResultItems = this.ingredientRepository.All().OrderBy(x => x.Name);

            bool isPrevFiltered = false;

            if (!string.IsNullOrEmpty(text))
            {
                isPrevFiltered = true;
                var searchIngredientByNameResultItems = this.GetByNamesQueryable(text);

                varResultItems = searchIngredientByNameResultItems;
            }

            if (!string.IsNullOrEmpty(ingredients))
            {
                var searchIngredientByNameResultItems = this.GetByIdsQueryable(ingredients);

                if (isPrevFiltered)
                {
                    varResultItems = from objA in varResultItems
                                      join objB in searchIngredientByNameResultItems on objA.Id equals objB.Id
                                      select objA;
                }
                else
                {
                    varResultItems = searchIngredientByNameResultItems;
                }
            }

            return varResultItems.To<T>().ToList();
        }

        private IQueryable<Ingredient> GetByIdsQueryable(string inputList)
        {
            var inputArray = inputList.Split(new char[] { ',', ' ' }, System.StringSplitOptions.RemoveEmptyEntries).ToList();

            var ingredients = this.ingredientRepository.All();
            ingredients = from ingr in ingredients
                          where inputArray.Any(input => ingr.Id.Equals(input))
                          select ingr;
            return ingredients;
        }

        private IQueryable<Ingredient> GetByNamesQueryable(string inputList)
        {
            var inputArray = inputList.Split(new char[] { ',', ' ' }, System.StringSplitOptions.RemoveEmptyEntries).ToList();

            var query = this.ingredientRepository.All();
            foreach (string input in inputArray)
            {
                query = query
                .Where(ingr => ingr.Name.Contains(input))
                .OrderBy(ingr => ingr.Name);
            }

            return query;
        }
    }
}
