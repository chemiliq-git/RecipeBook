namespace RecipeBook.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using RecipeBook.Data.Models;
    using RecipeBook.Services.Data;
    using RecipeBook.Web.ViewModels.Recipe;
    using System;
    using System.Threading.Tasks;

    public class RecipeController : Controller
    {
        private readonly IRecipeService recipeService;
        private readonly IRecipeTypeService recipeTypeService;
        private readonly ICookingHistoryService cookingHistoryService;
        private readonly UserManager<ApplicationUser> userManager;

        public RecipeController(IRecipeService recipeService, IRecipeTypeService recipeTypeService, ICookingHistoryService cookingHistoryService, UserManager<ApplicationUser> userManager)
        {
            this.recipeService = recipeService;
            this.recipeTypeService = recipeTypeService;
            this.cookingHistoryService = cookingHistoryService;
            this.userManager = userManager;
        }

        public ActionResult Index(string Id)
        {
            RecipeViewModel data = this.recipeService.GetById<RecipeViewModel>(Id);
            return this.View(data);
        }

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
            await this.recipeService.UpdateLastCookedDate(id, dateTimeNow);

            var currentRecipe = this.recipeService.GetById<RecipeViewModel>(id);

            CookingHistory cookingRecord = new CookingHistory();
            cookingRecord.RecipeId = currentRecipe.Id;
            cookingRecord.LastCooked = dateTimeNow;
            cookingRecord.RecipeEasyRate = currentRecipe.EasyRate;
            cookingRecord.RecipeTasteRate = currentRecipe.TasteRate;
            cookingRecord.UserId = this.userManager.GetUserId(this.User);

            await this.cookingHistoryService.CreateAsync(cookingRecord);

            return this.Json(dateTimeNow.ToString("s"));

        }
    }
}
