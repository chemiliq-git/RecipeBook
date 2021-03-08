namespace RecipeBook.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
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
        private readonly IVoteService voteService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ICookingHistoryService cookingHistoryService;

        public HomeController(IRecipeTypeService recipeTypeService, IRecipeService recipeService, IIngredientService ingredientsService,
            IIngredientTypeService ingredientTypeService, IVoteService voteService, UserManager<ApplicationUser> userManager, ICookingHistoryService cookingHistoryService)
        {
            this.recipeTypeService = recipeTypeService;
            this.recipeService = recipeService;
            this.ingredientsService = ingredientsService;
            this.ingredientTypeService = ingredientTypeService;
            this.voteService = voteService;
            this.userManager = userManager;
            this.cookingHistoryService = cookingHistoryService;
        }

        public IActionResult Index()
        {
            var indexViewModel = new IndexViewModel();
            indexViewModel.RecipeTypes = this.recipeTypeService.GetAll<IndexRecipeTypeViewModel>();
            return this.View(indexViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Search(SearchDataModel searchData)
        {
            var searchViewModel = new SearchViewModel();
            searchViewModel.SearchData = searchData;

            if (!this.ModelState.IsValid)
            {
                return this.View(searchViewModel);
            }

            if (searchData != null && !string.IsNullOrEmpty(searchData.Text))
            {
                var searchRecipesByNameResultItems = this.recipeService.GetByName<SearchResultItemViewModel>(searchData.Text);
                var searchRecipesByIngredientsResultItems = this.recipeService.GetByIngredients<SearchResultItemViewModel>(searchData.Text);

                searchViewModel.ResultItems = searchRecipesByNameResultItems.Union(searchRecipesByIngredientsResultItems);
            }
            else if (searchData != null && !string.IsNullOrEmpty(searchData.RecipeTypes))
            {
                var searchRecipesByTypesResultItems = this.recipeService.GetByRecipeTypes<SearchResultItemViewModel>(searchData.RecipeTypes);
                searchViewModel.ResultItems = searchRecipesByTypesResultItems;
            }
            else
            {
                searchViewModel.ResultItems = this.recipeService.GetAll<SearchResultItemViewModel>();
            }

            searchViewModel.ResultItems = searchViewModel.ResultItems.OrderByDescending(result => result.RecipeScore);
            return this.View(searchViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("SideBarSearch")]
        public IActionResult SideBarSearch(SearchDataModel searchData)
        {
            var searchViewModel = new SearchViewModel();

            searchViewModel.SearchData = searchData;

            //TODO change it
            //if (!this.ModelState.IsValid)
            //{
            //    return this.View("Search", searchViewModel);
            //}

            List<SearchResultItemViewModel> varResultItems = this.recipeService.GetAll<SearchResultItemViewModel>().ToList();
            bool isPrevFiltered = false;

            if (searchData != null && !string.IsNullOrEmpty(searchData.Text))
            {
                isPrevFiltered = true;
                var searchRecipesByNameResultItems = this.recipeService.GetByNamesList<SearchResultItemViewModel>(searchData.Text);

                var searchRecipesByIngredientsResultItems = this.recipeService.GetByIngredients<SearchResultItemViewModel>(searchData.Text);

                varResultItems = searchRecipesByNameResultItems.Union(searchRecipesByIngredientsResultItems).ToList();
            }

            if (searchData != null && !string.IsNullOrEmpty(searchData.RecipeTypes))
            {
                var searchRecipesByTypesResultItems = this.recipeService.GetByRecipeTypes<SearchResultItemViewModel>(searchData.RecipeTypes);

                if (isPrevFiltered)
                {
                    varResultItems = (from objA in varResultItems
                                      join objB in searchRecipesByTypesResultItems on objA.Id equals objB.Id
                                      select objA).ToList();
                }
                else
                {
                    varResultItems = searchRecipesByTypesResultItems.ToList();
                }

                isPrevFiltered = true;
            }

            if (searchData != null && !string.IsNullOrEmpty(searchData.Ingredients))
            {
                var searchRecipesByIngredientsResultItems = this.recipeService.GetByIngredients<SearchResultItemViewModel>(searchData.Ingredients);

                if (isPrevFiltered)
                {
                    varResultItems = (from objA in varResultItems
                                      join objB in searchRecipesByIngredientsResultItems on objA.Id equals objB.Id
                                      select objA).ToList();
                }
                else
                {
                    varResultItems = searchRecipesByIngredientsResultItems.ToList();
                }
            }


            searchViewModel.ResultItems = varResultItems.OrderByDescending(result => result.RecipeScore);
            var parView = this.PartialView("ResultList", searchViewModel);
            return parView;
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddRecipeToMenu(string id)
        {
            bool result = await this.recipeService.AddRecipeToMenu(id);

            return this.Json(new { @id = id, @result = result });
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RemoveRecipeFromMenu(string id)
        {
            bool result = await this.recipeService.RemoveRecipeFromMenu(id);

            return this.Json(new { @id = id, @result = result });
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UpdateLastCookedDate(string id, SearchDataModel searchData)
        {
            DateTime dateTimeNow = DateTime.UtcNow;
            var result = await this.recipeService.UpdateLastCookedDate(id, dateTimeNow);
            if (result)
            {
                var currentRecipe = this.recipeService.GetById<SearchResultItemViewModel>(id);

                CookingHistory cookingRecord = new CookingHistory();
                cookingRecord.RecipeId = currentRecipe.Id;
                cookingRecord.LastCooked = dateTimeNow;
                cookingRecord.RecipeEasyRate = currentRecipe.EasyRate;
                cookingRecord.RecipeTasteRate = currentRecipe.TasteRate;
                cookingRecord.UserId = this.userManager.GetUserId(this.User);

                await this.cookingHistoryService.CreateAsync(cookingRecord);
            }

            var searchViewModel = new SearchViewModel();

            searchViewModel.SearchData = searchData;

            List<SearchResultItemViewModel> varResultItems = this.recipeService.GetAll<SearchResultItemViewModel>().ToList();
            bool isPrevFiltered = false;

            if (searchData != null && !string.IsNullOrEmpty(searchData.Text))
            {
                isPrevFiltered = true;
                var searchRecipesByNameResultItems = this.recipeService.GetByNamesList<SearchResultItemViewModel>(searchData.Text);

                var searchRecipesByIngredientsResultItems = this.recipeService.GetByIngredients<SearchResultItemViewModel>(searchData.Text);

                varResultItems = searchRecipesByNameResultItems.Union(searchRecipesByIngredientsResultItems).ToList();
            }

            if (searchData != null && !string.IsNullOrEmpty(searchData.RecipeTypes))
            {
                var searchRecipesByTypesResultItems = this.recipeService.GetByRecipeTypes<SearchResultItemViewModel>(searchData.RecipeTypes);

                if (isPrevFiltered)
                {
                    varResultItems = (from objA in varResultItems
                                      join objB in searchRecipesByTypesResultItems on objA.Id equals objB.Id
                                      select objA).ToList();
                }
                else
                {
                    varResultItems = searchRecipesByTypesResultItems.ToList();
                }

                isPrevFiltered = true;
            }

            if (searchData != null && !string.IsNullOrEmpty(searchData.Ingredients))
            {
                var searchRecipesByIngredientsResultItems = this.recipeService.GetByIngredients<SearchResultItemViewModel>(searchData.Ingredients);

                if (isPrevFiltered)
                {
                    varResultItems = (from objA in varResultItems
                                      join objB in searchRecipesByIngredientsResultItems on objA.Id equals objB.Id
                                      select objA).ToList();
                }
                else
                {
                    varResultItems = searchRecipesByIngredientsResultItems.ToList();
                }
            }


            searchViewModel.ResultItems = varResultItems.OrderByDescending(result => result.RecipeScore);
            var parView = this.PartialView("ResultList", searchViewModel);
            return parView;
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
