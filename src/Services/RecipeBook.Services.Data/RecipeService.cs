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

        public IEnumerable<T> GetByInput<T>(string input)
        {
            IQueryable<Recipe> query =
               this.recipeRepository.All()
                .Where(r => r.Name.Contains(input))
                .OrderBy(x => x.Name);

            var result = query.To<T>().ToList();

            return result;
        }

        public IEnumerable<T> GetByInputList<T>(List<string> inputList)
        {
            IQueryable<Recipe> query = this.recipeRepository.All();
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
