namespace RecipeBook.Web.Controllers
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using RecipeBook.Services.Data;
    using RecipeBook.Web.ViewModels.Recipe;
    using System;

    public class RecipeController : Controller
    {
        private readonly IRecipeService recipeService;

        public RecipeController(IRecipeService recipeService)
        {
            this.recipeService = recipeService;
        }
        public ActionResult Index(string recipeId)
        {
            RecipeViewModel data = this.recipeService.GetById<RecipeViewModel>(recipeId);
            return this.View(data);
        }

        public ActionResult Create(int recipeId)
        {
            RecipeViewModel data = new RecipeViewModel();
            data.Id = Guid.NewGuid().ToString();

            return this.View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return this.RedirectToAction(nameof(Index));
            }
            catch
            {
                return this.View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return this.RedirectToAction(nameof(Index));
            }
            catch
            {
                return this.View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return this.RedirectToAction(nameof(Index));
            }
            catch
            {
                return this.View();
            }
        }
    }
}
