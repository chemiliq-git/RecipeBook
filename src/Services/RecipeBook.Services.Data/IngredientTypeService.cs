using RecipeBook.Data.Common.Repositories;
using RecipeBook.Data.Models;
using RecipeBook.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RecipeBook.Services.Data
{
    public class IngredientTypeService : IIngredientTypeService
    {
        private readonly IDeletableEntityRepository<IngredientType> ingredientTypeRepository;

        public IngredientTypeService(IDeletableEntityRepository<IngredientType> ingredientTypeRepository)
        {
            this.ingredientTypeRepository = ingredientTypeRepository;
        }

        public IEnumerable<T> GetAll<T>()
        {
            IQueryable<IngredientType> query =
               this.ingredientTypeRepository.All().OrderBy(x => x.Name);

            return query.To<T>().ToList();
        }

        public IEnumerable<T> GetByInput<T>(string input)
        {
            IQueryable<IngredientType> query =
               this.ingredientTypeRepository.All()
                .Where(r => r.Name.Contains(input))
                .OrderBy(x => x.Name);
            return query.To<T>().ToList();
        }
    }
}
