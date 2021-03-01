namespace RecipeBook.Services.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using RecipeBook.Data.Common.Repositories;
    using RecipeBook.Data.Models;

    public class CookingHistoryService : ICookingHistoryService
    {
        private readonly IRepository<CookingHistory> cookingHistoryRepository;

        public CookingHistoryService(IRepository<CookingHistory> cookingHistoryRepository)
        {
            this.cookingHistoryRepository = cookingHistoryRepository;
        }

        public async Task<bool> CreateAsync(CookingHistory input)
        {
            try
            {
                var existingRecord = this.cookingHistoryRepository.All()
                     .Where(r => r.RecipeId.Equals(input.RecipeId) && r.LastCooked.Equals(input.LastCooked));
                if (input.LastCooked != DateTime.MinValue && existingRecord.Count() == 0)
                {
                    await this.cookingHistoryRepository.AddAsync(input);
                    await this.cookingHistoryRepository.SaveChangesAsync();
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
