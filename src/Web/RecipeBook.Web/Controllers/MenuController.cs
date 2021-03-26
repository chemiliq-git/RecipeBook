namespace RecipeBook.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using RecipeBook.Data.Models;
    using RecipeBook.Services.Data;
    using RecipeBook.Web.ViewModels.Menu;

    public class MenuController : Controller
    {
        private readonly IRecipeService recipeService;
        private readonly ICookingHistoryService cookingHistoryService;
        private readonly UserManager<ApplicationUser> userManager;

        public MenuController(IRecipeService recipeService, ICookingHistoryService cookingHistoryService, UserManager<ApplicationUser> userManager)
        {
            this.recipeService = recipeService;
            this.cookingHistoryService = cookingHistoryService;
            this.userManager = userManager;
        }

        [Authorize]
        public ActionResult Index()
        {
            IndexViewModel data = new IndexViewModel();
            data.AllItems = this.recipeService.GetByIsInMenu<IndexItemViewModel>().OrderByDescending(result => result.RecipeScore).ToList();
            return this.View(data);
        }

        public ActionResult Details(string id)
        {
            // TODO
            return this.View();
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RemoveRecipeFromMenu(string id)
        {
            bool result = await this.recipeService.RemoveRecipeFromMenu(id);

            IndexViewModel data = new IndexViewModel();
            data.AllItems = this.recipeService.GetByIsInMenu<IndexItemViewModel>().OrderByDescending(result => result.RecipeScore).ToList();

            return this.View("Index", data);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UpdateLastCookedDate(string id)
        {
            IndexViewModel data = new IndexViewModel();
            DateTime dateTimeNow = DateTime.UtcNow;
            var result = await this.recipeService.UpdateLastCookedDate(id, dateTimeNow);
            if (result)
            {
                var currentRecipe = this.recipeService.GetById<IndexItemViewModel>(id);

                CookingHistory cookingRecord = new CookingHistory();
                cookingRecord.RecipeId = currentRecipe.Id;
                cookingRecord.LastCooked = dateTimeNow;
                cookingRecord.RecipeEasyRate = currentRecipe.EasyRate;
                cookingRecord.RecipeTasteRate = currentRecipe.TasteRate;
                cookingRecord.UserId = this.userManager.GetUserId(this.User);

                await this.cookingHistoryService.CreateAsync(cookingRecord);
            }

            data.AllItems = this.recipeService.GetByIsInMenu<IndexItemViewModel>().OrderByDescending(result => result.RecipeScore).ToList();
            return this.View("Index", data);
        }
    }
}
