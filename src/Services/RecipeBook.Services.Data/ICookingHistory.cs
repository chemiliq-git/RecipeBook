namespace RecipeBook.Services.Data
{
    using System.Threading.Tasks;
    using RecipeBook.Data.Models;

    public interface ICookingHistoryService
    {
        Task<bool> CreateAsync(CookingHistory input);
    }
}
