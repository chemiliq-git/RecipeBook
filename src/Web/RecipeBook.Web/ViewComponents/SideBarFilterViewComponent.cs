namespace RecipeBook.Web.ViewComponents
{
    using System.Linq;

    using Microsoft.AspNetCore.Mvc;
    using RecipeBook.Services.Data;
    using RecipeBook.Web.ViewModels.Common;
    using RecipeBook.Web.ViewModels.Home;

    [ViewComponent(Name = "SideBarFilter")]
    public class SideBarFilterViewComponent : ViewComponent
    {
        private readonly IRecipeTypeService recipeTypeService;
        private readonly IIngredientTypeService ingredientTypeService;

        public SideBarFilterViewComponent(IRecipeTypeService recipeTypeService, IIngredientTypeService ingredientTypeService)
        {
            this.recipeTypeService = recipeTypeService;
            this.ingredientTypeService = ingredientTypeService;
        }

        public IViewComponentResult Invoke(SearchDataModel searchData)
        {
            SideBarFilterViewModel sideBarFilterViewModel = new SideBarFilterViewModel();

            sideBarFilterViewModel.Mode = searchData.Mode;

            if (searchData.Mode == SearchDataModeEnum.Recipe)
            {
                sideBarFilterViewModel.RecipeTypes = this.recipeTypeService.GetAll<SearchRecipeTypeViewModel>();
                sideBarFilterViewModel.IngredientTypes = this.ingredientTypeService.GetAll<SearchIngredientTypeViewModel>();


                if (!string.IsNullOrEmpty(searchData.Text))
                {
                    sideBarFilterViewModel.SearchedText = searchData.Text;
                }

                if (!string.IsNullOrEmpty(searchData.RecipeTypes))
                {
                    sideBarFilterViewModel.RecipeTypes = sideBarFilterViewModel.RecipeTypes.Select(t =>
                    {
                        if (searchData.RecipeTypes.Contains(t.Id))
                        {
                            t.Checked = true;
                        }

                        return t;
                    });
                }

                if (!string.IsNullOrEmpty(searchData.Ingredients))
                {
                    foreach (SearchIngredientTypeViewModel ingredientType in sideBarFilterViewModel.IngredientTypes)
                    {
                        ingredientType.Ingredients = ingredientType.Ingredients.Select(
                            ingr =>
                            {
                                if (searchData.Ingredients.Contains(ingr.Id))
                                {
                                    ingr.Checked = true;
                                    ingredientType.Checked = true;
                                }

                                return ingr;
                            }).ToList();
                    }
                }
            }
            else
            {
                sideBarFilterViewModel.IngredientTypes = this.ingredientTypeService.GetAll<SearchIngredientTypeViewModel>();

                if (!string.IsNullOrEmpty(searchData.Text))
                {
                    sideBarFilterViewModel.SearchedText = searchData.Text;
                }

                if (!string.IsNullOrEmpty(searchData.Ingredients))
                {
                    foreach (SearchIngredientTypeViewModel ingredientType in sideBarFilterViewModel.IngredientTypes)
                    {
                        ingredientType.Ingredients = ingredientType.Ingredients.Select(
                            ingr =>
                            {
                                if (searchData.Ingredients.Contains(ingr.Id))
                                {
                                    ingr.Checked = true;
                                    ingredientType.Checked = true;
                                }

                                return ingr;
                            }).ToList();
                    }
                }
            }

            return this.View(sideBarFilterViewModel);
        }

    }
}
