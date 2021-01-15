namespace RecipeBook.Web.Controllers
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using RecipeBook.Services.Data;
    using RecipeBook.Web.ViewModels.Recipe;
    using System;
    using System.Threading.Tasks;

    public class RecipeController : Controller
    {
        private readonly IRecipeService recipeService;
        private readonly IRecipeTypeService recipeTypeService;

        public RecipeController(IRecipeService recipeService, IRecipeTypeService recipeTypeService)
        {
            this.recipeService = recipeService;
            this.recipeTypeService = recipeTypeService;
        }
        public ActionResult Index(string recipeId)
        {
            RecipeViewModel data = this.recipeService.GetById<RecipeViewModel>(recipeId);
            return this.View(data);
        }

        public ActionResult Create()
        {
            RecipeViewModel data = new RecipeViewModel();
            data.Id = Guid.NewGuid().ToString();
            data.AllRecipeTypes = this.recipeTypeService.GetAll<RecipeTypeViewModel>();
            data.IngredientSet = new Data.Models.IngredientsSet();


            return this.View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(RecipeViewModel inputData)
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    await this.recipeService.CreateAsync(new CreateRecipeDataModel()
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
                }

                // redirect to next view
                return this.RedirectToAction(nameof(Index), inputData.Id);
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
