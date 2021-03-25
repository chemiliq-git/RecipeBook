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

        [Authorize]
        public IActionResult Index()
        {
            var indexViewModel = new IndexViewModel();
            indexViewModel.RecipeTypes = this.recipeTypeService.GetAll<IndexRecipeTypeViewModel>();
            return this.View(indexViewModel);
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
