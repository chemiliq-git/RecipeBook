namespace RecipeBook.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using RecipeBook.Data.Models;
    using RecipeBook.Services.Data;
    using RecipeBook.Web.ViewModels.Recipe;
    using RecipeBook.Web.ViewModels.Ingredeint;
    using RecipeBook.Web.ViewModels.Ingredient;
    using RecipeBook.Web.ViewModels.Common;

    public class IngredientsController : Controller
    {
        private readonly IIngredientService ingredientsService;
        private readonly IIngredientTypeService ingredientTypeService;

        public IngredientsController(IIngredientService ingredientsService, IIngredientTypeService ingredientTypeService)
        {
            this.ingredientsService = ingredientsService;
            this.ingredientTypeService = ingredientTypeService;
        }

        // GET: Ingredients
        public IActionResult Index()
        {
            ViewModels.Ingredeint.IndexViewModel data = new ViewModels.Ingredeint.IndexViewModel();
            data.SearchData = new SearchDataModel();
            data.SearchData.Mode = SearchDataModeEnum.Ingredient;

            data.ResultItems = this.ingredientsService.GetAll<IngredientViewModel>().ToList();

            return this.View(data);
        }

        // GET: Ingredients/Details/5
        public IActionResult Details(string id)
        {
            IngredientViewModel data = new IngredientViewModel();
            var result = this.ingredientsService.GetById<IngredientViewModel>(id);

            if (result.Count() > 0)
            {
                data = result.ToList()[0];
            }
            else
            {
                return this.NotFound();
            }

            return this.View(data);
        }

        // GET: Ingredients/Create
        [Authorize]
        public IActionResult Create()
        {
            IngredientViewModel data = new IngredientViewModel();

            data.AllIngredientType = this.ingredientTypeService.GetAll<IngredientTypeViewModel>().ToList();

            return this.View(data);
        }

        // POST: Ingredients/Create
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IngredientViewModel input)
        {
            if (this.ModelState.IsValid)
            {
                Ingredient ingredient = new Ingredient();
                ingredient.Id = input.Id;
                ingredient.Name = input.Name;
                ingredient.ImagePath = input.ImagePath;
                ingredient.IngredientTypeID = input.IngredientTypeID;
                var result = await this.ingredientsService.CreateAsync(ingredient);

                if (result)
                {
                    return this.RedirectToAction(nameof(this.Details), new {@Id = input.Id });
                }
            }

            return this.View(input);
        }

        // GET: Ingredients/Edit/5
        public IActionResult Edit(string id)
        {
            IngredientViewModel data = new IngredientViewModel();
            var result = this.ingredientsService.GetById<IngredientViewModel>(id);

            if (result.Count() > 0)
            {
                data = result.ToList()[0];
                data.AllIngredientType = this.ingredientTypeService.GetAll<IngredientTypeViewModel>().ToList();
            }
            else
            {
                return this.NotFound();
            }

            return this.View(data);
        }

        // POST: Ingredients/Edit/5
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(IngredientViewModel input)
        {
            if (this.ModelState.IsValid)
            {
                Ingredient ingredient = new Ingredient();
                ingredient.Id = input.Id;
                ingredient.Name = input.Name;
                ingredient.ImagePath = input.ImagePath;
                ingredient.IngredientTypeID = input.IngredientTypeID;
                var result = await this.ingredientsService.UpdateAsync(ingredient);

                if (result)
                {
                    return this.RedirectToAction(nameof(this.Details), new { @id=input.Id});
                }
            }

            return this.View(input);
        }

        // GET: Ingredients/Delete/5
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(string id, SearchDataModel searchData)
        {
            var result = await this.ingredientsService.DeleteAsync(id);

            if (searchData.Ingredients != null && searchData.Ingredients.Contains(id))
            {
                searchData.Ingredients.Replace(id, string.Empty);
            }

            ViewModels.Ingredeint.IndexViewModel data = new ViewModels.Ingredeint.IndexViewModel();
            data.SearchData = searchData;
            data.SearchData.Mode = SearchDataModeEnum.Ingredient;


            List<IngredientViewModel> varResultItems = this.ingredientsService.GetAll<IngredientViewModel>().ToList();
            bool isPrevFiltered = false;

            if (searchData != null && !string.IsNullOrEmpty(searchData.Text))
            {
                isPrevFiltered = true;
                var searchIngredientByNameResultItems = this.ingredientsService.GetByNamesList<IngredientViewModel>(searchData.Text);

                varResultItems = searchIngredientByNameResultItems.ToList();
            }

            if (searchData != null && !string.IsNullOrEmpty(searchData.Ingredients))
            {
                var searchIngredientByNameResultItems = this.ingredientsService.GetByIdList<IngredientViewModel>(searchData.Ingredients);

                if (isPrevFiltered)
                {
                    varResultItems = (from objA in varResultItems
                                      join objB in searchIngredientByNameResultItems on objA.Id equals objB.Id
                                      select objA).ToList();
                }
                else
                {
                    varResultItems = searchIngredientByNameResultItems.ToList();
                }
            }

            data.ResultItems = varResultItems;

            return this.View(nameof(this.Index), data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("SideBarSearch")]
        public IActionResult SideBarSearch(SearchDataModel searchData)
        {
            var indexViewModel = new ViewModels.Ingredeint.IndexViewModel();

            indexViewModel.SearchData = searchData;

            //TODO
            //if (!this.ModelState.IsValid)
            //{
            //    return this.View(indexViewModel);
            //}

            List<IngredientViewModel> varResultItems = this.ingredientsService.GetAll<IngredientViewModel>().ToList();
            bool isPrevFiltered = false;

            if (searchData != null && !string.IsNullOrEmpty(searchData.Text))
            {
                isPrevFiltered = true;
                var searchIngredientByNameResultItems = this.ingredientsService.GetByNamesList<IngredientViewModel>(searchData.Text);

                varResultItems = searchIngredientByNameResultItems.ToList();
            }

            if (searchData != null && !string.IsNullOrEmpty(searchData.Ingredients))
            {
                var searchIngredientByNameResultItems = this.ingredientsService.GetByIdList<IngredientViewModel>(searchData.Ingredients);

                if (isPrevFiltered)
                {
                    varResultItems = (from objA in varResultItems
                                      join objB in searchIngredientByNameResultItems on objA.Id equals objB.Id
                                      select objA).ToList();
                }
                else
                {
                    varResultItems = searchIngredientByNameResultItems.ToList();
                }
            }

            indexViewModel.ResultItems = varResultItems;
            var parView = this.PartialView("IndexResultList", indexViewModel);
            return parView;
        }
    }
}
