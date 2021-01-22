namespace RecipeBook.Web.Controllers
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    using Microsoft.AspNetCore.Mvc;
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

        public HomeController(IRecipeTypeService recipeTypeService, IRecipeService recipeService, IIngredientService ingredientsService,
            IIngredientTypeService ingredientTypeService, IVoteService voteService)
        {
            this.recipeTypeService = recipeTypeService;
            this.recipeService = recipeService;
            this.ingredientsService = ingredientsService;
            this.ingredientTypeService = ingredientTypeService;
            this.voteService = voteService;
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

            return this.View(searchViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("SideBarSearch")]
        public IActionResult SideBarSearch(SearchDataModel searchData)
        {
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


            searchViewModel.ResultItems = varResultItems;
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
