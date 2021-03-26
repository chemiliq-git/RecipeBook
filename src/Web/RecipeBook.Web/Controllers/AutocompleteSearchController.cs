namespace RecipeBook.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.AspNetCore.Mvc;
    using RecipeBook.Services.Data;
    using RecipeBook.Web.ViewModels.Common;
    using RecipeBook.Web.ViewModels.Home;
    using RecipeBook.Web.ViewModels.Recipe;

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
               var searchRecipesResultItems = this.recipeService.GetByNamesList<IndexRecipeItemViewModel>(inputText).ToList();

               return this.Json(searchRecipesResultItems);
            }
            else
            {
                var searchRecipesResultItems = this.ingredientsService.GetByNamesList<IndexIngredientItemViewModel>(inputText).ToList();

                return this.Json(searchRecipesResultItems);
            }
        }
    }
}
