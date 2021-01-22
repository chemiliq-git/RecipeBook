namespace RecipeBook.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.Json;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using RecipeBook.Services.Data;
    using RecipeBook.Web.ViewModels.Home;
    using RecipeBook.Web.ViewModels.IngredientsSet;

    public class IngredientsSetController : Controller
    {
        private readonly IIngredientService ingredientsService;
        private readonly IIngredientsSetService ingredientsSetService;

        public IngredientsSetController(IIngredientService ingredientsService, IIngredientsSetService ingredientsSetService)
        {
            this.ingredientsService = ingredientsService;
            this.ingredientsSetService = ingredientsSetService;
        }

        // GET: IngredientsSetController
        public ActionResult Index(string id)
        {
            IngredientsSetScreenViewModel data = new IngredientsSetScreenViewModel();
            data.SearchData = new SearchDataModel();
            data.SearchData.Mode = SearchDataModeEnum.Product;

            data.SearchResultItems = this.ingredientsService.GetAll<SearchResultItemViewModel>();

            var ingredientsSets = this.ingredientsSetService.GetByRecipeId<IngredientsSetViewModel>(id);

            if (ingredientsSets.Count() > 0)
            {
                data.IngredientsSet = ingredientsSets.First();
            }
            else
            {
                data.IngredientsSet = new IngredientsSetViewModel();
                data.IngredientsSet.Id = Guid.NewGuid().ToString();
                data.IngredientsSet.IngredientSetItems = new List<IngredientsSetItemViewModel>();
            }

            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Add(string selectedId, string data)
        {
            IngredientsSetScreenViewModel viewModelData = new IngredientsSetScreenViewModel();

            if (!string.IsNullOrEmpty(data))
            {
                var options = new JsonSerializerOptions
                {
                    AllowTrailingCommas = true,
                    MaxDepth = 5,
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,

                };
                viewModelData = JsonSerializer.Deserialize<IngredientsSetScreenViewModel>(data, options);
            }

            IngredientsSetItemViewModel selectedIngredient = new IngredientsSetItemViewModel();
            var result = this.ingredientsService.GetById<IngredientViewModel>(selectedId);
            if (result.Count() > 0)
            {
                selectedIngredient.Ingredient = result.ToList()[0];
            }

            if (viewModelData.IngredientsSet == null)
            {
                viewModelData.IngredientsSet = new IngredientsSetViewModel();
                viewModelData.IngredientsSet.IngredientSetItems = new List<IngredientsSetItemViewModel>();
            }

            viewModelData.IngredientsSet.IngredientSetItems.Add(selectedIngredient);
            var parViewStr = await ControllerExtensions.RenderViewToStringAsync(this, "IngredientsSetItemsList", viewModelData);

            var res = this.Json(new { @partialView = parViewStr, @data = viewModelData });

            return res;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Remove(string selectedId, string data)
        {
            IngredientsSetScreenViewModel viewModelData = new IngredientsSetScreenViewModel();

            if (!string.IsNullOrEmpty(data))
            {
                var options = new JsonSerializerOptions
                {
                    AllowTrailingCommas = true,
                    MaxDepth = 5,
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,

                };
                viewModelData = JsonSerializer.Deserialize<IngredientsSetScreenViewModel>(data, options);
            }

            var selectedItem = viewModelData.IngredientsSet.IngredientSetItems.Where(i => i.Id == selectedId).ToList()[0];
            viewModelData.IngredientsSet.IngredientSetItems.Remove(selectedItem);

            var parViewStr = await ControllerExtensions.RenderViewToStringAsync(this, "IngredientsSetItemsList", viewModelData);

            var res = this.Json(new { @partialView = parViewStr, @data = viewModelData });

            return res;
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("SideBarSearch")]
        public IActionResult SideBarSearch(SearchDataModel searchData)
        {
            IngredientsSetScreenViewModel data = new IngredientsSetScreenViewModel();

            data.SearchData = searchData;

            List<SearchResultItemViewModel> varResultItems = this.ingredientsService.GetAll<SearchResultItemViewModel>().ToList();
            bool isPrevFiltered = false;

            if (searchData != null && !string.IsNullOrEmpty(searchData.Text))
            {
                isPrevFiltered = true;
                var searchIngredientByNameResultItems = this.ingredientsService.GetByNamesList<SearchResultItemViewModel>(searchData.Text);

                varResultItems = searchIngredientByNameResultItems.ToList();
            }

            if (searchData != null && !string.IsNullOrEmpty(searchData.Ingredients))
            {
                var searchIngredientByNameResultItems = this.ingredientsService.GetByNamesList<SearchResultItemViewModel>(searchData.Ingredients);

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


            data.SearchResultItems = varResultItems;
            var parView = this.PartialView("SearchResultList", data);
            return parView;
        }
        // GET: IngredientsSetController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: IngredientsSetController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: IngredientsSetController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: IngredientsSetController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: IngredientsSetController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: IngredientsSetController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
