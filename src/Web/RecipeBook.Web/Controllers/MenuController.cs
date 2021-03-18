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
            MenuViewModel data = new MenuViewModel();
            data.AllItems = this.recipeService.GetByIsInMenu<MenuRecipeViewModel>().OrderByDescending(result => result.RecipeScore).ToList();
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

            MenuViewModel data = new MenuViewModel();
            data.AllItems = this.recipeService.GetByIsInMenu<MenuRecipeViewModel>().OrderByDescending(result => result.RecipeScore).ToList();

            return this.View("Index", data);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UpdateLastCookedDate(string id)
        {
            MenuViewModel data = new MenuViewModel();
            DateTime dateTimeNow = DateTime.UtcNow;
            var result = await this.recipeService.UpdateLastCookedDate(id, dateTimeNow);
            if (result)
            {
                var currentRecipe = this.recipeService.GetById<MenuRecipeViewModel>(id);

                CookingHistory cookingRecord = new CookingHistory();
                cookingRecord.RecipeId = currentRecipe.Id;
                cookingRecord.LastCooked = dateTimeNow;
                cookingRecord.RecipeEasyRate = currentRecipe.EasyRate;
                cookingRecord.RecipeTasteRate = currentRecipe.TasteRate;
                cookingRecord.UserId = this.userManager.GetUserId(this.User);

                await this.cookingHistoryService.CreateAsync(cookingRecord);
            }

            data.AllItems = this.recipeService.GetByIsInMenu<MenuRecipeViewModel>().OrderByDescending(result => result.RecipeScore).ToList();
            return this.View("Index", data);
        }
    }
}
