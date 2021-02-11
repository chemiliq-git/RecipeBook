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

        public IEnumerable<T> GetByNamesList<T>(string inputList)
        {
            List<string> inputArray = new List<string>();
            var result = new List<T>();

            if (!string.IsNullOrEmpty(inputList))
            {
                inputArray = inputList.Split(new char[] { ',', ' ' }, System.StringSplitOptions.RemoveEmptyEntries).ToList();

                IQueryable<Ingredient> query = this.ingredientRepository.All();
                foreach (string input in inputArray)
                {
                    query = query
                    .Where(ingr => ingr.Name.Contains(input))
                    .OrderBy(ingr => ingr.Name);
                }

                result = query.To<T>().ToList();
            }

            return result;
        }

        public IEnumerable<T> GetByIdList<T>(string inputList)
        {
            List<string> inputArray = new List<string>();
            var result = new List<T>();

            if (!string.IsNullOrEmpty(inputList))
            {
                inputArray = inputList.Split(new char[] { ',', ' ' }, System.StringSplitOptions.RemoveEmptyEntries).ToList();

                IQueryable<Ingredient> query = this.ingredientRepository.All();
                foreach (string input in inputArray)
                {
                    query = query
                    .Where(ingr => ingr.Id.Equals(input))
                    .OrderBy(ingr => ingr.Name);
                }

                result = query.To<T>().ToList();
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

    }
}
