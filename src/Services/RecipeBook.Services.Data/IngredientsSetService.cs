namespace RecipeBook.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using RecipeBook.Data.Common.Repositories;
    using RecipeBook.Data.Models;
    using RecipeBook.Services.Mapping;
    using RecipeBook.Services.Data;
    using System;

    public class IngredientsSetService : IIngredientsSetService
    {
        private readonly IDeletableEntityRepository<IngredientsSet> ingredientsSetRepository;
        private readonly IDeletableEntityRepository<IngredientsSetItem> ingredientsSetItemRepository;

        public IngredientsSetService(IDeletableEntityRepository<IngredientsSet> ingredientsSetRepository, IDeletableEntityRepository<IngredientsSetItem> ingredientsSetItemRepository)
        {
            this.ingredientsSetRepository = ingredientsSetRepository;
            this.ingredientsSetItemRepository = ingredientsSetItemRepository;
        }

        public IEnumerable<T> GetAll<T>()
        {
            IQueryable<IngredientsSet> query =
              this.ingredientsSetRepository.All().OrderBy(x => x.Name);

            return query.To<T>().ToList();
        }

        public IEnumerable<T> GetById<T>(string input)
        {
            IQueryable<IngredientsSet> query =
               this.ingredientsSetRepository.All()
                .Where(r => r.Id.Equals(input))
                .OrderBy(x => x.Name);
            return query.To<T>().ToList();
        }

        public IEnumerable<T> GetByRecipeId<T>(string input)
        {
            IQueryable<IngredientsSet> query =
               this.ingredientsSetRepository.All()
                .Where(r => r.RecipeID.Equals(input))
                .OrderBy(x => x.Name);
            return query.To<T>().ToList();
        }

        public async Task<bool> CreateAsync(IngredientsSet input)
        {
            try
            {
                var dateTimeNow = DateTime.Now;

                IngredientsSet ingredientsSet = new IngredientsSet();
                ingredientsSet.Id = input.Id;
                ingredientsSet.Name = input.Name;
                ingredientsSet.RecipeID = input.RecipeID;
                ingredientsSet.IngredientSetItems = input.IngredientSetItems;
                ingredientsSet.CreatedOn = dateTimeNow;

                await this.ingredientsSetRepository.AddAsync(ingredientsSet);
                await this.ingredientsSetRepository.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateAsync(IngredientsSet input, List<IngredientsSetItemDataModel> ingredientSetItems)
        {
            try
            {
                var dateTimeNow = DateTime.Now;
                IngredientsSet ingredientsSet = new IngredientsSet();
                ingredientsSet.Id = input.Id;
                ingredientsSet.Name = input.Name;
                ingredientsSet.RecipeID = input.RecipeID;

                this.ingredientsSetRepository.Update(ingredientsSet);
                foreach (var item in ingredientSetItems)
                {
                    if (item.Status == -1)
                    {
                        IngredientsSetItem vItem = new IngredientsSetItem();
                        vItem.Id = item.Id;
                        vItem.IngredientID = item.IngredientID;
                        vItem.QTYData = item.QTYData;
                        vItem.IngredientsSetId = ingredientsSet.Id;
                        this.ingredientsSetItemRepository.Delete(vItem);
                    }
                    else if (item.Status == 1)
                    {
                        IngredientsSetItem vItem = new IngredientsSetItem();
                        vItem.Id = item.Id;
                        vItem.IngredientID = item.IngredientID;
                        vItem.QTYData = item.QTYData;
                        vItem.IngredientsSetId = ingredientsSet.Id;
                        vItem.CreatedOn = dateTimeNow;
                        vItem.IsMainItem = item.IsMainItem;
                        await this.ingredientsSetItemRepository.AddAsync(vItem);
                    }
                    else
                    {
                        IngredientsSetItem vItem = new IngredientsSetItem();
                        vItem.Id = item.Id;
                        vItem.IngredientID = item.IngredientID;
                        vItem.QTYData = item.QTYData;
                        vItem.IngredientsSetId = ingredientsSet.Id;
                        vItem.IsMainItem = item.IsMainItem;
                        this.ingredientsSetItemRepository.Update(vItem);
                    }
                }

                await this.ingredientsSetRepository.SaveChangesAsync();
                await this.ingredientsSetItemRepository.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
