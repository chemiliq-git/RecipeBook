namespace RecipeBook.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using RecipeBook.Data.Common.Repositories;
    using RecipeBook.Data.Models;

    public class VoteService : IVoteService
    {
        private readonly IRepository<Vote> voteRepository;

        public VoteService(IRepository<Vote> voteRepository)
        {
            this.voteRepository = voteRepository;
        }

        public async Task VoteAsync(string recipeId, string userId, VoteTypeEnm voteType, int value)
        {
            var vote = this.voteRepository.All()
                 .FirstOrDefault(x => x.RecipeId == recipeId && x.UserId == userId && x.Type == voteType);
            if (vote != null)
            {
                vote.Value = value;
            }
            else
            {
                vote = new Vote
                {
                    Id = Guid.NewGuid().ToString(),
                    RecipeId = recipeId,
                    UserId = userId,
                    Type = voteType,
                    Value = value,
                };

                await this.voteRepository.AddAsync(vote);
            }

            await this.voteRepository.SaveChangesAsync();
        }

        public int GetValue(string recipeId, VoteTypeEnm voteType)
        {
            var votes = this.voteRepository.All()
               .Where(v => v.RecipeId == recipeId && v.Type == voteType)
               .Average(v => v.Value);
            return Convert.ToInt32(votes);
        }

        //public Dictionary<T> GetRecipesTasteVoteValues<T>(ICollection<Recipe> recipes)
        //{
        //    var result = from vote in this.voteRepository.All()
        //                join recipe in recipes
        //                on vote.RecipeId equals recipe.Id
        //                where vote.Type == VoteTypeEnm.Taste
        //                group vote by recipe.Id into voteGroup
        //                select new 
        //                {
        //                    RecipeId= voteGroup.Key,
        //                    Average = voteGroup.Average(vote => vote.Value),
        //                };

        //    return ;
        //}
    }
}
