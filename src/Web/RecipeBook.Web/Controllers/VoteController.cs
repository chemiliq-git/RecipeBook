namespace RecipeBook.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using RecipeBook.Data.Models;
    using RecipeBook.Services.Data;
    using RecipeBook.Web.ViewModels.Recipe;

    [Route("api/[controller]")]
    [ApiController]
    public class VoteController : Controller
    {
        private readonly IVoteService voteService;
        private readonly UserManager<ApplicationUser> userManager;

        public VoteController(IVoteService voteService, UserManager<ApplicationUser> userManager)
        {
            this.voteService = voteService;
            this.userManager = userManager;
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult<int>> Post([FromForm]VoteInputModel input)
        {
            var userId = this.userManager.GetUserId(this.User);
            await this.voteService.VoteAsync(input.RecipeId, userId, input.Type, input.Value);
            var votesValue = this.voteService.GetValue(input.RecipeId, input.Type);
            return this.Json(votesValue);
        }

    }
}
