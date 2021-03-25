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
            if (searchDataMode.ToUpper() == SearchDataModeEnum.Recipe.ToString().ToUpper())
            {
               var searchRecipesResultItems = this.recipeService.GetByNamesList<RecipesSearchResultItemViewModel>(inputText).ToList();

               return this.Json(searchRecipesResultItems);
            }
            else
            {
                var searchRecipesResultItems = this.ingredientsService.GetByNamesList<IngredientsSearchResultItemViewModel>(inputText).ToList();

                return this.Json(searchRecipesResultItems);
            }
        }
    }
}
