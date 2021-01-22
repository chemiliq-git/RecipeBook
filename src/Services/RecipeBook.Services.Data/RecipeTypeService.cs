namespace RecipeBook.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using RecipeBook.Data.Common.Repositories;
    using RecipeBook.Data.Models;
    using RecipeBook.Services.Mapping;

    public class RecipeTypeService : IRecipeTypeService
    {
        private readonly IDeletableEntityRepository<RecipeType> recipeTypeRepository;

        public RecipeTypeService(IDeletableEntityRepository<RecipeType> recipeTypeRepository)
        {
            this.recipeTypeRepository = recipeTypeRepository;
        }

        public List<T> GetAll<T>()
        {
            IQueryable<RecipeType> query =
                this.recipeTypeRepository.All().OrderBy(x => x.Name);

            return query.To<T>().ToList();
        }

    }
}
