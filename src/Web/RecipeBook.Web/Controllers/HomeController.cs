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

        public HomeController(IRecipeTypeService recipeTypeService)
        {
            this.recipeTypeService = recipeTypeService;
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
