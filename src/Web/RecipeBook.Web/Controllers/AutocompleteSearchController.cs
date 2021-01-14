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
        public ActionResult<IEnumerable<string>> Post([FromForm]string inputText)
        {
            var searchRecipesResultItems = this.recipeService.GetByNamesList<SearchResultItemViewModel>(inputText);
            //var searchIngredientsResultItems = this.ingredientsService.GetByInputList<SearchResultItemViewModel>(inputText);

            var resultItems = /*searchIngredientsResultItems.Concat(*/searchRecipesResultItems/*)*/;

            return this.Json(resultItems);
        }
    }
}
