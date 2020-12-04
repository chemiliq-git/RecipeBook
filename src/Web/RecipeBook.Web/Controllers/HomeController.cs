namespace RecipeBook.Web.Controllers
{
    using System.Diagnostics;

    using Microsoft.AspNetCore.Mvc;
    using RecipeBook.Services.Data;
    using RecipeBook.Web.ViewModels;
    using RecipeBook.Web.ViewModels.Home;

    public class HomeController : BaseController
    {
        private readonly IRecipeTypeService recipeTypeService;

        public HomeController(IRecipeTypeService recipeTypeService)
        {
            this.recipeTypeService = recipeTypeService;
        }

        public IActionResult Index()
        {
            var indexViewModel = new IndexViewModel();
            indexViewModel.RecipeTypes = this.recipeTypeService.GetAllTypes<IndexRecipeTypeViewModel>();
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
