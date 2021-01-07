namespace RecipeBook.Services.Data
{
    using System.Threading.Tasks;
    using RecipeBook.Data.Models;

    public interface IVoteService
    {
        Task VoteAsync(string recipeId, string userId, VoteTypeEnm voteType, int value);

        int GetValue(string recipeId, VoteTypeEnm voteType);
    }
}
