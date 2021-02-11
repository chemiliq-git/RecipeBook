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
    public class AutocompleteSearchController : Controller
    {
        private readonly IRecipeService recipeService;
        private readonly IIngredientService ingredientsService;

        public AutocompleteSearchController(IRecipeService recipeService, IIngredientService ingredientsService)
        {
            this.recipeService = recipeService;
            this.ingredientsService = ingredientsService;
        }

        // [HttpGet("{input}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult<IEnumerable<string>> Post([FromForm]string inputText, [FromForm] string searchDataMode)
        {
            var searchRecipesResultItems = new List<SearchResultItemViewModel>();
            if (searchDataMode.ToUpper() == SearchDataModeEnum.Recipe.ToString().ToUpper())
            {
                searchRecipesResultItems = this.recipeService.GetByNamesList<SearchResultItemViewModel>(inputText).ToList();
            }
            else if (searchDataMode.ToUpper() == SearchDataModeEnum.Ingredient.ToString().ToUpper())
            {
                searchRecipesResultItems = this.ingredientsService.GetByNamesList<SearchResultItemViewModel>(inputText).ToList();
            }

            var resultItems = searchRecipesResultItems;

            return this.Json(resultItems);
        }
    }
}
