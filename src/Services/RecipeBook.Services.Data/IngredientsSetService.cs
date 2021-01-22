namespace RecipeBook.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using RecipeBook.Data.Common.Repositories;
    using RecipeBook.Data.Models;
    using RecipeBook.Services.Mapping;

    public class IngredientsSetService : IIngredientsSetService
    {
        private readonly IDeletableEntityRepository<IngredientsSet> ingredientsSetRepository;

        public IngredientsSetService(IDeletableEntityRepository<IngredientsSet> ingredientsSetRepository)
        {
            this.ingredientsSetRepository = ingredientsSetRepository;
        }

        public IEnumerable<T> GetAll<T>()
        {
            IQueryable<IngredientsSet> query =
              this.ingredientsSetRepository.All().OrderBy(x => x.Name);

            return query.To<T>().ToList();
        }

        public IEnumerable<T> GetByRecipeId<T>(string input)
        {
            IQueryable<IngredientsSet> query =
               this.ingredientsSetRepository.All()
                .Where(r => r.Id.Equals(input))
                .OrderBy(x => x.Name);
            return query.To<T>().ToList();
        }
    }
}
