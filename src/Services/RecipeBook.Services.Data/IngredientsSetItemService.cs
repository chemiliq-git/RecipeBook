namespace RecipeBook.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using RecipeBook.Data.Common.Repositories;
    using RecipeBook.Data.Models;
    using RecipeBook.Services.Mapping;

    public class IngredientsSetItemService : IIngredientsSetItemService
    {
        private readonly IDeletableEntityRepository<IngredientsSetItem> ingredientsSetItemRepository;

        public IngredientsSetItemService(IDeletableEntityRepository<IngredientsSetItem> ingredientsSetItemRepository)
        {
            this.ingredientsSetItemRepository = ingredientsSetItemRepository;
        }

        public IEnumerable<T> GetByIngredientId<T>(string input)
        {
            IQueryable<IngredientsSetItem> query =
               this.ingredientsSetItemRepository.All()
                .Where(i => i.Ingredient.Id.Equals(input))
                .OrderBy(i => i.Ingredient.Name);
            return query.To<T>().ToList();
        }
    }
}
