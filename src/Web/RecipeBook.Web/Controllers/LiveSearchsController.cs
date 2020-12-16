namespace RecipeBook.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;

    using RecipeBook.Data;
    using RecipeBook.Data.Models;
    using RecipeBook.Services.Data;
    using RecipeBook.Web.ViewModels.Home;

    [Route("api/[controller]")]
    [ApiController]
    public class LiveSearchsController : ControllerBase
    {
        private readonly ApplicationDbContext applicationDbContext;
        private readonly IRecipeService recipeService;
        private readonly IIngredientService ingredientsService;

        public LiveSearchsController(ApplicationDbContext applicationDbContext, IRecipeService recipeService, IIngredientService ingredientsService)
        {
            this.applicationDbContext = applicationDbContext;
            this.recipeService = recipeService;
            this.ingredientsService = ingredientsService;
        }

        // [HttpGet("{input}")]
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get(string input)
        {
            var result = this.applicationDbContext.Recipes
                .Select(r => new { ID = r.Id, Name = r.Name, Text = r.Text })
                .Where(r => (r.Name.Contains(input) || r.Text.Contains(input)))
                .Select(n => new string(n.ID + ";" + n.Name))
                .ToList();

            return result;
        }
    }
}
