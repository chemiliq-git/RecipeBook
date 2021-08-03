namespace RecipeBook.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using RecipeBook.Data.Models;
    using RecipeBook.Services.Data;
    using RecipeBook.Web.ViewModels.Recipe;
    using RecipeBook.Web.ViewModels.Common;
    using Microsoft.Extensions.Logging;

    public class RecipeController : Controller
    {
        private readonly IRecipeService recipeService;
        private readonly IRecipeTypeService recipeTypeService;
        private readonly ICookingHistoryService cookingHistoryService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ILogger<RecipeController> logger;

        public RecipeController(IRecipeService recipeService, IRecipeTypeService recipeTypeService, 
            ICookingHistoryService cookingHistoryService, UserManager<ApplicationUser> userManager, 
            ILogger<RecipeController> logger)
        {
            this.recipeService = recipeService;
            this.recipeTypeService = recipeTypeService;
            this.cookingHistoryService = cookingHistoryService;
            this.userManager = userManager;
            this.logger = logger;
        }

        [Authorize]
        public ActionResult Index()
        {
            this.logger.LogInformation($"-----------------------------Test_RecipeController_log");
            var searchViewModel = new IndexViewModel();
            searchViewModel.SearchData = new SearchDataModel();
            searchViewModel.SearchData.Mode = SearchDataModeEnum.Recipe;
            searchViewModel.ResultItems = this.recipeService.GetAll<IndexRecipeItemViewModel>();

            searchViewModel.ResultItems = searchViewModel.ResultItems.OrderByDescending(result => result.RecipeScore);
            return this.View(searchViewModel);
        }

        public IActionResult Details(string id)
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}

            //var recipe = await _context.Recipes
            //    .Include(r => r.RecipeType)
            //    .FirstOrDefaultAsync(m => m.Id == id);
            //if (recipe == null)
            //{
            //    return NotFound();
            //}

            //return View(recipe);
            RecipeViewModel data = this.recipeService.GetById<RecipeViewModel>(id);
            return this.View(data);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Search(SearchDataModel searchData)
        {
            var searchViewModel = new IndexViewModel();
            searchViewModel.SearchData = searchData;

            if (!this.ModelState.IsValid)
            {
                return this.View(searchViewModel);
            }

            if (searchData != null)
            {
                searchViewModel.ResultItems = this.recipeService.GetFromHomeSearchData<IndexRecipeItemViewModel>(searchData.Text, searchData.RecipeTypes);
            }

            searchViewModel.ResultItems = searchViewModel.ResultItems.OrderByDescending(result => result.RecipeScore);
            return this.View("Index", searchViewModel);
        }

        [Authorize]
        public ActionResult Create()
        {
            RecipeViewModel data = new RecipeViewModel();
            data.AllRecipeTypes = this.recipeTypeService.GetAll<RecipeTypeViewModel>();
            data.IngredientSet = new ViewModels.IngredientsSet.IngredientsSetViewModel();

            return this.View(data);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(RecipeViewModel inputData)
        {
            if (this.ModelState.IsValid)
            {
                bool result = await this.recipeService.CreateAsync(new RecipeDataModel()
                {
                    Id = inputData.Id,
                    ImagePath = inputData.ImagePath,
                    Name = inputData.Name,
                    Text = inputData.Text,
                    RecipeTypeId = inputData.RecipeType.Id,
                    //IngredientRecipeTypes = inputData.IngredientRecipeTypes,
                    //IngredientSetId = inputData.IngredientSet.Id,
                    LastCooked = inputData.LastCooked,
                });

                if (result)
                {
                    CookingHistory cookingRecord = new CookingHistory();
                    cookingRecord.RecipeId = inputData.Id;
                    cookingRecord.LastCooked = inputData.LastCooked;
                    cookingRecord.RecipeEasyRate = inputData.EasyRate;
                    cookingRecord.RecipeTasteRate = inputData.TasteRate;
                    cookingRecord.UserId = this.userManager.GetUserId(this.User);
                    await this.cookingHistoryService.CreateAsync(cookingRecord);

                    // redirect to next view
                    return this.RedirectToAction(nameof(this.Edit), "Recipe", new { @id = inputData.Id });
                }
            }

            return this.View(inputData);
        }

        public ActionResult Edit(string Id)
        {
            RecipeViewModel data = this.recipeService.GetById<RecipeViewModel>(Id);
            data.AllRecipeTypes = this.recipeTypeService.GetAll<RecipeTypeViewModel>();

            return this.View(data);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(RecipeViewModel inputData)
        {
            if (this.ModelState.IsValid)
            {
                bool result = await this.recipeService.UpdateAsync(new RecipeDataModel()
                {
                    Id = inputData.Id,
                    ImagePath = inputData.ImagePath,
                    Name = inputData.Name,
                    Text = inputData.Text,
                    RecipeTypeId = inputData.RecipeType.Id,
                    PreparationTime = inputData.PreparationTime,
                    //IngredientRecipeTypes = inputData.IngredientRecipeTypes,
                    //IngredientSetId = inputData.IngredientSet.Id,
                    LastCooked = inputData.LastCooked,
                });

                if (result)
                {
                    CookingHistory cookingRecord = new CookingHistory();
                    cookingRecord.RecipeId = inputData.Id;
                    cookingRecord.LastCooked = inputData.LastCooked;
                    cookingRecord.RecipeEasyRate = inputData.EasyRate;
                    cookingRecord.RecipeTasteRate = inputData.TasteRate;
                    cookingRecord.UserId = this.userManager.GetUserId(this.User);
                    await this.cookingHistoryService.CreateAsync(cookingRecord);

                    return this.RedirectToAction(nameof(this.Index), "Recipe", new { @id = inputData.Id });
                }
            }

            return this.View(inputData);
        }

        //[HttpPost]
        //[Authorize]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(string id)
        //{

        //    return this.View();
        //}

        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(string id)
        //{
        //    return RedirectToAction(/*nameof(Index)*/);
        //}

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UpdateLastCookedDate(string id)
        {
            DateTime dateTimeNow = DateTime.UtcNow;
            var result = await this.recipeService.UpdateLastCookedDate(id, dateTimeNow);
            if (result)
            {
                var currentRecipe = this.recipeService.GetById<RecipeViewModel>(id);

                CookingHistory cookingRecord = new CookingHistory();
                cookingRecord.RecipeId = currentRecipe.Id;
                cookingRecord.LastCooked = dateTimeNow;
                cookingRecord.RecipeEasyRate = currentRecipe.EasyRate;
                cookingRecord.RecipeTasteRate = currentRecipe.TasteRate;
                cookingRecord.UserId = this.userManager.GetUserId(this.User);

                await this.cookingHistoryService.CreateAsync(cookingRecord);

                return this.Json(new { @result = dateTimeNow.ToString("s") });
            }
            else
            {
                return this.Json(new { @result = string.Empty });
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("SideBarSearch")]
        public IActionResult SideBarSearch(SearchDataModel searchData)
        {
            var searchViewModel = new IndexViewModel();

            searchViewModel.SearchData = searchData;

            IEnumerable<IndexRecipeItemViewModel> varResultItems = this.recipeService.GetByNamesAndRecipeTypeIdsAndIngrIds<IndexRecipeItemViewModel>(
                searchData.Text, searchData.RecipeTypes, searchData.Ingredients);

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

        //[HttpPost]
        //[Authorize]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> UpdateLastCookedDate(string id)
        //{
        //    DateTime dateTimeNow = DateTime.UtcNow;
        //    var result = await this.recipeService.UpdateLastCookedDate(id, dateTimeNow);
        //    if (result)
        //    {
        //        var currentRecipe = this.recipeService.GetById<SearchResultItemViewModel>(id);

        //        CookingHistory cookingRecord = new CookingHistory();
        //        cookingRecord.RecipeId = currentRecipe.Id;
        //        cookingRecord.LastCooked = dateTimeNow;
        //        cookingRecord.RecipeEasyRate = currentRecipe.EasyRate;
        //        cookingRecord.RecipeTasteRate = currentRecipe.TasteRate;
        //        cookingRecord.UserId = this.userManager.GetUserId(this.User);

        //        await this.cookingHistoryService.CreateAsync(cookingRecord);
        //    }

        //    return this.Json(new { @result = result });
        //}

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteRecipe(string id)
        {
            var result = await this.recipeService.DeleteAsync(id);
            return this.Json(new { @result = result });
        }
    }
}
