namespace RecipeBook.Web.Controllers
{
	using System.Linq;
	using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using RecipeBook.Services.Data;
	using RecipeBook.Web.ViewModels.Menu;

	public class MenuController : Controller
    {
        private readonly IRecipeService recipeService;

        public MenuController(IRecipeService recipeService)
        {
            this.recipeService = recipeService;
        }

        public ActionResult Index()
        {
            MenuViewModel data = new MenuViewModel();
            data.AllItems = this.recipeService.GetByIsInMenu<MenuRecipeViewModel>().ToList();
            return this.View(data);
        }

        public ActionResult Details(string id)
        {

            return View();
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RemoveRecipeFromMenu(string id)
        {
            bool result = await this.recipeService.RemoveRecipeFromMenu(id);

            MenuViewModel data = new MenuViewModel();
            data.AllItems = this.recipeService.GetByIsInMenu<MenuRecipeViewModel>().ToList();

            return this.View("Index", data);
        }
    }
}
