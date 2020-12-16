namespace RecipeBook.Web.Controllers
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using RecipeBook.Data;
    using RecipeBook.Data.Models;
    using RecipeBook.Services.Data;
    using RecipeBook.Web.ViewModels;
    using RecipeBook.Web.ViewModels.Home;

    public class HomeController : Controller
    {
        private readonly IRecipeTypeService recipeTypeService;
        private readonly IRecipeService recipeService;
        private readonly IIngredientService ingredientsService;
        private readonly IIngredientTypeService ingredientTypeService;

        public HomeController(IRecipeTypeService recipeTypeService, IRecipeService recipeService, IIngredientService ingredientsService, IIngredientTypeService ingredientTypeService)
        {
            this.recipeTypeService = recipeTypeService;
            this.recipeService = recipeService;
            this.ingredientsService = ingredientsService;
            this.ingredientTypeService = ingredientTypeService;
        }

        public IActionResult Index()
        {
            var indexViewModel = new IndexViewModel();
            indexViewModel.RecipeTypes = this.recipeTypeService.GetAll<IndexRecipeTypeViewModel>();
            return this.View(indexViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Search(string inputText)
        {
            var searchViewModel = new SearchViewModel();
            var searchRecipesResultItems = this.recipeService.GetByInput<SearchItemViewModel>(inputText);
            foreach (var item in searchRecipesResultItems)
            {
                item.Type = typeof(Recipe).ToString();
            }

            var searchIngredientsResultItems = this.ingredientsService.GetByInput<SearchItemViewModel>(inputText);
            foreach (var item in searchIngredientsResultItems)
            {
                item.Type = typeof(Ingredient).ToString();
            }

            searchViewModel.ResultItems = searchIngredientsResultItems.Concat(searchRecipesResultItems);
            searchViewModel.RecipeTypes = this.recipeTypeService.GetAll<SearchRecipeTypeViewModel>();
            searchViewModel.IngredientTypes = this.ingredientTypeService.GetAll<SearchIngredientTypeViewModel>();

            return this.View(searchViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("SearchPartial")]
        public IActionResult SearchPartial(string inputText)
        {
            List<string> inputArray = new List<string>();
            if (!string.IsNullOrEmpty(inputText))
            {
                inputArray = inputText.Split(',', System.StringSplitOptions.RemoveEmptyEntries).ToList();
            }

            var searchViewModel = new SearchViewModel();
            var searchRecipesResultItems = this.recipeService.GetByInputList<SearchItemViewModel>(inputArray);
            foreach (var item in searchRecipesResultItems)
            {
                item.Type = typeof(Recipe).ToString();
            }

            var searchIngredientsResultItems = this.ingredientsService.GetByInputList<SearchItemViewModel>(inputArray);
            foreach (var item in searchIngredientsResultItems)
            {
                item.Type = typeof(Ingredient).ToString();
            }

            searchViewModel.ResultItems = searchIngredientsResultItems.Concat(searchRecipesResultItems);

            return this.PartialView("ResultList", searchViewModel);
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
