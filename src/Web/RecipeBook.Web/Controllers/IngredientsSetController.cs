namespace RecipeBook.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.Json;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using RecipeBook.Data.Models;
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

        public ActionResult Create(string id)
        {
            IngredientsSetScreenViewModel data = new IngredientsSetScreenViewModel();
            data.SearchData = new SearchDataModel();
            data.SearchData.Mode = SearchDataModeEnum.Ingredient;

            data.SearchResultItems = this.ingredientsService.GetAll<SearchResultItemViewModel>().ToList();
            data.IngredientsSetId = Guid.NewGuid().ToString();
            // TODO change IngredientsSetName
            data.IngredientsSetName = "Set";
            data.RecipeId = id;

            return this.View(data);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(IngredientsSetScreenViewModel inputData)
        {
            if (this.ModelState.IsValid)
            {
                List<IngredientsSetItem> ingredientsSetItems = new List<IngredientsSetItem>();
                foreach (var item in inputData.IngredientsSetItems)
                {
                    IngredientsSetItem ingrSetItem = new IngredientsSetItem
                    {
                        Id = item.Id,
                        IngredientID = item.IngredientId,
                        QTYData = item.QTYData,
                        IsMainItem = item.IsMainItem,
                    };
                    ingredientsSetItems.Add(ingrSetItem);
                }

                bool result = await this.ingredientsSetService.CreateAsync(new Data.Models.IngredientsSet
                {
                    Id = inputData.IngredientsSetId,
                    Name = inputData.IngredientsSetName,
                    IngredientSetItems = ingredientsSetItems,
                    RecipeID = inputData.RecipeId,
                });

                if (result)
                {
                    return this.RedirectToAction(nameof(this.Edit), new { @id = inputData.RecipeId });
                }
            }

            return this.View(inputData);
        }

        // GET: IngredientsSetController/Edit/5
        public ActionResult Edit(string Id)
        {
            IngredientsSetScreenViewModel data = new IngredientsSetScreenViewModel();
            data.SearchData = new SearchDataModel();
            data.SearchData.Mode = SearchDataModeEnum.Ingredient;

            data.SearchResultItems = this.ingredientsService.GetAll<SearchResultItemViewModel>().ToList();

            var ingredientsSets = this.ingredientsSetService.GetByRecipeId<IngredientsSetViewModel>(Id);

            if (ingredientsSets.Count() > 0)
            {
                data.IngredientsSetId = ingredientsSets.First().Id;
                data.IngredientsSetName = ingredientsSets.First().Name;
                data.IngredientsSetItems = ingredientsSets.First().IngredientSetItems.ToList();
            }
            else
            {
                data.IngredientsSetId = Guid.NewGuid().ToString();
                data.IngredientsSetName = "Set";
            }

            data.RecipeId = Id;

            return this.View(data);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(IngredientsSetScreenViewModel inputData)
        {
            if (this.ModelState.IsValid)
            {
                List<IngredientsSetItemDataModel> ingredientsSetItems = new List<IngredientsSetItemDataModel>();
                foreach (var item in inputData.IngredientsSetItems)
                {
                    IngredientsSetItemDataModel ingrSetItem = new IngredientsSetItemDataModel
                    {
                        Id = item.Id,
                        IngredientID = item.IngredientId,
                        QTYData = item.QTYData,
                        Status = item.Status,
                        IsMainItem = item.IsMainItem,
                    };
                    ingredientsSetItems.Add(ingrSetItem);
                }

                bool result = await this.ingredientsSetService.UpdateAsync(
                    new Data.Models.IngredientsSet
                    {
                        Id = inputData.IngredientsSetId,
                        Name = inputData.IngredientsSetName,
                        RecipeID = inputData.RecipeId,
                    }, ingredientsSetItems);

                if (result)
                {
                    return this.RedirectToAction(nameof(this.Edit), new { @id = inputData.RecipeId });
                }
            }

            return this.View(inputData);
        }

        // GET: IngredientsSetController/Delete/5
        public ActionResult Delete(int id)
        {
            // TODO
            return View();
        }

        // POST: IngredientsSetController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            // TODO
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<ActionResult> Add(string selectedId, string modelData)
        {
            IngredientsSetScreenViewModel viewModelData = new IngredientsSetScreenViewModel();

            if (!string.IsNullOrEmpty(modelData))
            {
                var options = new JsonSerializerOptions
                {
                    AllowTrailingCommas = true,
                    MaxDepth = 5,
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,

                };
                viewModelData = JsonSerializer.Deserialize<IngredientsSetScreenViewModel>(modelData, options);
            }

            IngredientsSetItemViewModel ingredeintsSetItem = new IngredientsSetItemViewModel();
            var result = this.ingredientsService.GetById<IngredientViewModel>(selectedId);
            if (result.Count() > 0)
            {
                ingredeintsSetItem.IngredientId = result.ToList()[0].Id;
                ingredeintsSetItem.IngredientName = result.ToList()[0].Name;
            }

            ingredeintsSetItem.Status = 1;
            viewModelData.IngredientsSetItems.Add(ingredeintsSetItem);
            var parViewStr = await ControllerExtensions.RenderViewToStringAsync(this, "IngredientsSetItemsList", viewModelData);

            var res = this.Json(new { @partialView = parViewStr, @data = viewModelData });

            return res;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Remove(string selectedId, string modelData)
        {
            IngredientsSetScreenViewModel viewModelData = new IngredientsSetScreenViewModel();

            if (!string.IsNullOrEmpty(modelData))
            {
                var options = new JsonSerializerOptions
                {
                    AllowTrailingCommas = true,
                    MaxDepth = 5,
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,

                };
                viewModelData = JsonSerializer.Deserialize<IngredientsSetScreenViewModel>(modelData, options);
            }

            var selectedItem = viewModelData.IngredientsSetItems.Where(i => i.Id == selectedId).ToList()[0];
            if (selectedItem.Status == 1)
            {
                viewModelData.IngredientsSetItems.Remove(selectedItem);
            }
            else
            {
                selectedItem.Status = -1;
            }
           

            var parViewStr = await ControllerExtensions.RenderViewToStringAsync(this, "IngredientsSetItemsList", viewModelData);

            var res = this.Json(new { @partialView = parViewStr, @data = viewModelData });

            return res;
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("SideBarSearch")]
        public async Task<IActionResult> SideBarSearch(SearchDataModel searchData, string modelData)
        {
            IngredientsSetScreenViewModel data = new IngredientsSetScreenViewModel();

            if (!string.IsNullOrEmpty(modelData))
            {
                var options = new JsonSerializerOptions
                {
                    AllowTrailingCommas = true,
                    MaxDepth = 5,
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,

                };
                data = JsonSerializer.Deserialize<IngredientsSetScreenViewModel>(modelData, options);
            }

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
                var searchIngredientByNameResultItems = this.ingredientsService.GetByIdList<SearchResultItemViewModel>(searchData.Ingredients);

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


            var parViewStr = await ControllerExtensions.RenderViewToStringAsync(this, "SearchResultList", data);

            var res = this.Json(new { @partialView = parViewStr, @data = data });

            return res;
        }
    }
}
