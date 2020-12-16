namespace RecipeBook.Services.Data
{
    using RecipeBook.Data.Common.Repositories;
    using RecipeBook.Data.Models;
    using RecipeBook.Services.Mapping;
    using System;
    using System.Collections.Generic;
    using System.Linq;

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

        public IEnumerable<T> GetByInput<T>(string input)
        {
            IQueryable<Ingredient> query =
               this.ingredientRepository.All()
                .Where(r => r.Name.Contains(input))
                .OrderBy(x => x.Name);
            return query.To<T>().ToList();
        }

        public IEnumerable<T> GetByInputList<T>(List<string> inputList)
        {
            IQueryable<Ingredient> query = this.ingredientRepository.All();
            foreach (var input in inputList)
            {
                query = query
                .Where(r => r.Name.Contains(input))
                .OrderBy(x => x.Name);
            }

            var result = query.To<T>().ToList();

            return result;
        }
    }
}
