namespace RecipeBook.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Security.Principal;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using RecipeBook.Data.Common.Repositories;
    using RecipeBook.Data.Models;
    using RecipeBook.Services.Mapping;

    public class RecipeService : IRecipeService
    {
        private readonly IDeletableEntityRepository<Recipe> recipeRepository;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IIdentity identity;

        public RecipeService(IDeletableEntityRepository<Recipe> recipeRepository, IHttpContextAccessor httpContextAccessor)
        {
            this.recipeRepository = recipeRepository;
            this.httpContextAccessor = httpContextAccessor;
        }

        public IEnumerable<T> GetAll<T>()
        {
            string currentUserUId = this.httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            IQueryable<Recipe> query =
               this.recipeRepository.All()
               .OrderBy(x => x.Name)
               .Select(r => new Recipe
               {
                   Id = r.Id,
                   Name = r.Name,
                   Text = r.Text,
                   ImagePath = r.ImagePath,
                   IngredientSetId = r.IngredientSetId,
                   LastCooked = r.LastCooked,
                   DeletedOn = r.DeletedOn,
                   ModifiedOn = r.ModifiedOn,
                   CreatedOn = r.CreatedOn,
                   IsDeleted = r.IsDeleted,
                   IngredientRecipeTypeId = r.IngredientRecipeTypeId,
                   IsInMenu = r.IsInMenu,
                   RecipeTypeId = r.RecipeTypeId,
                   PreparationTime = r.PreparationTime,
                   RecipeType = r.RecipeType,
                   IngredientSet = r.IngredientSet,
                   Votes = r.Votes,

                   TasteRate = r.Votes.Where(v => v.Type == VoteTypeEnm.Taste
                   && v.UserId == currentUserUId).ToList().Count > 0 ?
                   (int)r.Votes.Where(v => v.Type == VoteTypeEnm.Taste
                   && v.UserId == currentUserUId).First<Vote>().Value : 0,

                   EasyRate = r.Votes.Where(v => v.Type == VoteTypeEnm.Easy
                   && v.UserId == currentUserUId).ToList().Count > 0 ?
                   (int)r.Votes.Where(v => v.Type == VoteTypeEnm.Easy
                   && v.UserId == currentUserUId).First<Vote>().Value : 0,
               }); 
            return query.To<T>().ToList();
        }

        public T GetById<T>(string input)
        {
            string currentUserUId = this.httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            IQueryable<Recipe> query =
               this.recipeRepository.All()
                .Where(r => r.Id.Equals(input))
                .Select(r => new Recipe
                {
                    Id = r.Id,
                    Name = r.Name,
                    Text = r.Text,
                    ImagePath = r.ImagePath,
                    IngredientSetId = r.IngredientSetId,
                    LastCooked = r.LastCooked,
                    DeletedOn = r.DeletedOn,
                    ModifiedOn = r.ModifiedOn,
                    CreatedOn = r.CreatedOn,
                    IsDeleted = r.IsDeleted,
                    IngredientRecipeTypeId = r.IngredientRecipeTypeId,
                    IsInMenu = r.IsInMenu,
                    RecipeTypeId = r.RecipeTypeId,
                    PreparationTime = r.PreparationTime,
                    RecipeType = r.RecipeType,
                    IngredientSet = r.IngredientSet,
                    Votes = r.Votes,

                    TasteRate = r.Votes.Where(v => v.Type == VoteTypeEnm.Taste
                     && v.UserId == currentUserUId).ToList().Count > 0 ?
                   (int)r.Votes.Where(v => v.Type == VoteTypeEnm.Taste
                   && v.UserId == currentUserUId).First<Vote>().Value : 0,

                    EasyRate = r.Votes.Where(v => v.Type == VoteTypeEnm.Easy
                     && v.UserId == currentUserUId).ToList().Count > 0 ?
                   (int)r.Votes.Where(v => v.Type == VoteTypeEnm.Easy
                   && v.UserId == currentUserUId).First<Vote>().Value : 0,
                });


            var result = query.To<T>().ToList();

            return result[0];
        }

        public IEnumerable<T> GetByNames<T>(string inputList)
        {
            List<string> inputArray = new List<string>();
            var result = new List<T>();

            if (!string.IsNullOrEmpty(inputList))
            {
                var query = this.GetByNames(inputList);
                result = query.To<T>().ToList();
            }

            return result;
        }

        public IEnumerable<T> GetByIsInMenu<T>()
        {
            string currentUserUId = this.httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            List<string> inputArray = new List<string>();
            var result = new List<T>();

            var recipes = this.recipeRepository.All();
            recipes = from recipe in recipes
                      where recipe.IsInMenu == true
                      select new Recipe
                      {
                          Id = recipe.Id,
                          Name = recipe.Name,
                          Text = recipe.Text,
                          ImagePath = recipe.ImagePath,
                          IngredientSetId = recipe.IngredientSetId,
                          LastCooked = recipe.LastCooked,
                          DeletedOn = recipe.DeletedOn,
                          ModifiedOn = recipe.ModifiedOn,
                          CreatedOn = recipe.CreatedOn,
                          IsDeleted = recipe.IsDeleted,
                          IngredientRecipeTypeId = recipe.IngredientRecipeTypeId,
                          IsInMenu = recipe.IsInMenu,
                          RecipeTypeId = recipe.RecipeTypeId,
                          PreparationTime = recipe.PreparationTime,
                          RecipeType = recipe.RecipeType,
                          IngredientSet = recipe.IngredientSet,
                          Votes = recipe.Votes,

                          TasteRate = recipe.Votes.Where(v => v.Type == VoteTypeEnm.Taste
                          && v.UserId == currentUserUId).ToList().Count > 0 ?
                   (int)recipe.Votes.Where(v => v.Type == VoteTypeEnm.Taste
                   && v.UserId == currentUserUId).First<Vote>().Value : 0,

                          EasyRate = recipe.Votes.Where(v => v.Type == VoteTypeEnm.Easy
                          && v.UserId == currentUserUId).ToList().Count > 0 ?
                   (int)recipe.Votes.Where(v => v.Type == VoteTypeEnm.Easy
                   && v.UserId == currentUserUId).First<Vote>().Value : 0,
                      };
            result = recipes.To<T>().ToList();

            return result;
        }

        public IEnumerable<T> GetFromHomeSearchData<T>(string text, string recipeTypes)
        {
            if (!string.IsNullOrEmpty(text))
            {
                var searchRecipesByNameResultItems = this.GetByName(text);
                //var searchRecipesByIngredientsResultItems = this.GetByIngredientsNames(text);

                IQueryable<Recipe> result = searchRecipesByNameResultItems;//.Union(searchRecipesByIngredientsResultItems);
                return result.To<T>().ToList();
            }
            else if (!string.IsNullOrEmpty(recipeTypes))
            {
                List<T> result = this.GetByRecipeTypeIds<T>(recipeTypes).ToList();
                return result;
            }
            else
            {
                List<T> result = this.GetAll<T>().ToList();
                return result;
            }
        }

        public IEnumerable<T> GetByNamesAndRecipeTypeIdsAndIngrIds<T>(string text, string recipeTypes, string ingredients)
        {
            string currentUserUId = this.httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            IQueryable<Recipe> varResultItems = this.recipeRepository.All()
                .OrderBy(x => x.Name)
               .Select(r => new Recipe
               {
                   Id = r.Id,
                   Name = r.Name,
                   Text = r.Text,
                   ImagePath = r.ImagePath,
                   IngredientSetId = r.IngredientSetId,
                   LastCooked = r.LastCooked,
                   DeletedOn = r.DeletedOn,
                   ModifiedOn = r.ModifiedOn,
                   CreatedOn = r.CreatedOn,
                   IsDeleted = r.IsDeleted,
                   IngredientRecipeTypeId = r.IngredientRecipeTypeId,
                   IsInMenu = r.IsInMenu,
                   RecipeTypeId = r.RecipeTypeId,
                   PreparationTime = r.PreparationTime,
                   RecipeType = r.RecipeType,
                   IngredientSet = r.IngredientSet,
                   Votes = r.Votes,

                   TasteRate = r.Votes.Where(v => v.Type == VoteTypeEnm.Taste
                   && v.UserId == currentUserUId).ToList().Count > 0 ?
                   (int)r.Votes.Where(v => v.Type == VoteTypeEnm.Taste
                   && v.UserId == currentUserUId).First<Vote>().Value : 0,

                   EasyRate = r.Votes.Where(v => v.Type == VoteTypeEnm.Easy
                   && v.UserId == currentUserUId).ToList().Count > 0 ?
                   (int)r.Votes.Where(v => v.Type == VoteTypeEnm.Easy
                   && v.UserId == currentUserUId).First<Vote>().Value : 0,
               });

            bool isPrevFiltered = false;

            if (!string.IsNullOrEmpty(text))
            {
                isPrevFiltered = true;
                var searchRecipesByNameResultItems = this.GetByNames(text);

                //var searchRecipesByIngredientsResultItems = this.GetByIngredientsNames(text);

                varResultItems = searchRecipesByNameResultItems;//.Union(searchRecipesByIngredientsResultItems);
            }

            if (!string.IsNullOrEmpty(recipeTypes))
            {
                var searchRecipesByTypesResultItems = this.GetByRecipeTypeIds(recipeTypes);

                if (isPrevFiltered)
                {
                    varResultItems = from objA in varResultItems
                                     join objB in searchRecipesByTypesResultItems on objA.Id equals objB.Id
                                     select objA;
                }
                else
                {
                    varResultItems = searchRecipesByTypesResultItems;
                }

                isPrevFiltered = true;
            }

            if (!string.IsNullOrEmpty(ingredients))
            {
                var searchRecipesByIngredientsResultItems = this.GetByIngredientsIds(ingredients);

                if (isPrevFiltered)
                {
                    varResultItems = from objA in varResultItems
                                     join objB in searchRecipesByIngredientsResultItems on objA.Id equals objB.Id
                                     select objA;
                }
                else
                {
                    varResultItems = searchRecipesByIngredientsResultItems;
                }
            }

            return varResultItems.To<T>().ToList();
        }

        public async Task<bool> CreateAsync(RecipeDataModel inputRecipe)
        {
            try
            {
                Recipe recipe = new Recipe();
                recipe.Id = inputRecipe.Id;
                recipe.ImagePath = inputRecipe.ImagePath;
                recipe.Name = inputRecipe.Name;
                recipe.Text = inputRecipe.Text;
                recipe.RecipeTypeId = inputRecipe.RecipeTypeId;
                //recipe.IngredientRecipeTypeId = inputRecipe.;
                recipe.IngredientSetId = inputRecipe.IngredientSetId;
                recipe.LastCooked = inputRecipe.LastCooked;
                DateTime vNow = DateTime.UtcNow;
                recipe.CreatedOn = vNow;
                recipe.ModifiedOn = vNow;
                recipe.IsDeleted = false;
                await this.recipeRepository.AddAsync(recipe);
                await this.recipeRepository.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateAsync(RecipeDataModel inputRecipe)
        {
            try
            {
                var recipes = this.recipeRepository.All()
                .Where(r => r.Id == inputRecipe.Id)
                .ToList();

                if (recipes.Count > 0)
                {
                    recipes[0].Id = inputRecipe.Id;
                    recipes[0].ImagePath = inputRecipe.ImagePath;
                    recipes[0].Name = inputRecipe.Name;
                    recipes[0].Text = inputRecipe.Text;
                    recipes[0].RecipeTypeId = inputRecipe.RecipeTypeId;
                    recipes[0].IngredientSetId = inputRecipe.IngredientSetId;
                    recipes[0].PreparationTime = inputRecipe.PreparationTime;
                    recipes[0].LastCooked = inputRecipe.LastCooked;
                    DateTime vNow = DateTime.UtcNow;
                    recipes[0].ModifiedOn = vNow;
                    this.recipeRepository.Update(recipes[0]);
                    await this.recipeRepository.SaveChangesAsync();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteAsync(string inputId)
        {
            try
            {
                var recipes = this.recipeRepository.All()
                .Where(r => r.Id == inputId)
                .ToList();
                if (recipes.Count > 0)
                {
                    this.recipeRepository.Delete(recipes[0]);
                    await this.recipeRepository.SaveChangesAsync();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateLastCookedDate(string inputId, DateTime currentDateTime)
        {
            try
            {
                var recipes = this.recipeRepository.All()
                .Where(r => r.Id == inputId)
                .ToList();

                if (recipes.Count > 0)
                {
                    recipes[0].LastCooked = currentDateTime;
                    this.recipeRepository.Update(recipes[0]);
                    await this.recipeRepository.SaveChangesAsync();
                    return true;
                }

                return false;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> AddRecipeToMenu(string inputId)
        {
            try
            {
                var recipes = this.recipeRepository.All()
                .Where(r => r.Id == inputId)
                .ToList();

                if (recipes.Count > 0)
                {
                    recipes[0].IsInMenu = true;
                    this.recipeRepository.Update(recipes[0]);
                    await this.recipeRepository.SaveChangesAsync();
                    return true;
                }

                return false;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> RemoveRecipeFromMenu(string inputId)
        {
            try
            {
                var recipes = this.recipeRepository.All()
                .Where(r => r.Id == inputId)
                .ToList();

                if (recipes.Count > 0)
                {
                    recipes[0].IsInMenu = false;
                    this.recipeRepository.Update(recipes[0]);
                    await this.recipeRepository.SaveChangesAsync();
                    return true;
                }

                return false;
            }
            catch
            {
                return false;
            }
        }

        private IQueryable<Recipe> GetByRecipeTypeIds(string inputList)
        {
            string currentUserUId = this.httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var inputArray = inputList.Split(new char[] { ',', ' ' }, System.StringSplitOptions.RemoveEmptyEntries).ToList();

            var recipes = this.recipeRepository.All();
            recipes = from recipe in recipes
                      where inputArray.Any(input => recipe.RecipeTypeId.Equals(input))
                      select new Recipe
                      {
                          Id = recipe.Id,
                          Name = recipe.Name,
                          Text = recipe.Text,
                          ImagePath = recipe.ImagePath,
                          IngredientSetId = recipe.IngredientSetId,
                          LastCooked = recipe.LastCooked,
                          DeletedOn = recipe.DeletedOn,
                          ModifiedOn = recipe.ModifiedOn,
                          CreatedOn = recipe.CreatedOn,
                          IsDeleted = recipe.IsDeleted,
                          IngredientRecipeTypeId = recipe.IngredientRecipeTypeId,
                          IsInMenu = recipe.IsInMenu,
                          RecipeTypeId = recipe.RecipeTypeId,
                          PreparationTime = recipe.PreparationTime,
                          RecipeType = recipe.RecipeType,
                          IngredientSet = recipe.IngredientSet,
                          Votes = recipe.Votes,

                          TasteRate = recipe.Votes.Where(v => v.Type == VoteTypeEnm.Taste
                          && v.UserId == currentUserUId).ToList().Count > 0 ?
                   (int)recipe.Votes.Where(v => v.Type == VoteTypeEnm.Taste
                   && v.UserId == currentUserUId).First<Vote>().Value : 0,

                          EasyRate = recipe.Votes.Where(v => v.Type == VoteTypeEnm.Easy
                          && v.UserId == currentUserUId).ToList().Count > 0 ?
                   (int)recipe.Votes.Where(v => v.Type == VoteTypeEnm.Easy
                   && v.UserId == currentUserUId).First<Vote>().Value : 0,
                      };

            return recipes;
        }

        private IQueryable<Recipe> GetByIngredientsIds(string inputList)
        {
            string currentUserUId = this.httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            IQueryable<Recipe> recipes = this.recipeRepository.All();
            recipes = from recipe in recipes
                      where recipe.IngredientSet.IngredientSetItems.Any(ingrSetItem => inputList.Contains(ingrSetItem.IngredientID))
                      select new Recipe
                      {
                          Id = recipe.Id,
                          Name = recipe.Name,
                          Text = recipe.Text,
                          ImagePath = recipe.ImagePath,
                          IngredientSetId = recipe.IngredientSetId,
                          LastCooked = recipe.LastCooked,
                          DeletedOn = recipe.DeletedOn,
                          ModifiedOn = recipe.ModifiedOn,
                          CreatedOn = recipe.CreatedOn,
                          IsDeleted = recipe.IsDeleted,
                          IngredientRecipeTypeId = recipe.IngredientRecipeTypeId,
                          IsInMenu = recipe.IsInMenu,
                          RecipeTypeId = recipe.RecipeTypeId,
                          PreparationTime = recipe.PreparationTime,
                          RecipeType = recipe.RecipeType,
                          IngredientSet = recipe.IngredientSet,
                          Votes = recipe.Votes,

                          TasteRate = recipe.Votes.Where(v => v.Type == VoteTypeEnm.Taste
                          && v.UserId == currentUserUId).ToList().Count > 0 ?
                   (int)recipe.Votes.Where(v => v.Type == VoteTypeEnm.Taste
                   && v.UserId == currentUserUId).First<Vote>().Value : 0,

                          EasyRate = recipe.Votes.Where(v => v.Type == VoteTypeEnm.Easy
                          && v.UserId == currentUserUId).ToList().Count > 0 ?
                   (int)recipe.Votes.Where(v => v.Type == VoteTypeEnm.Easy
                   && v.UserId == currentUserUId).First<Vote>().Value : 0,
                      };
            return recipes;
        }

        private IQueryable<Recipe> GetByName(string input)
        {
            string currentUserUId = this.httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            return this.recipeRepository.All()
                            .Where(r => r.Name.ToUpper().Contains(input.ToUpper()))
                            .OrderBy(x => x.Name)
                            .Select(r => new Recipe
                            {
                                Id = r.Id,
                                Name = r.Name,
                                Text = r.Text,
                                ImagePath = r.ImagePath,
                                IngredientSetId = r.IngredientSetId,
                                LastCooked = r.LastCooked,
                                DeletedOn = r.DeletedOn,
                                ModifiedOn = r.ModifiedOn,
                                CreatedOn = r.CreatedOn,
                                IsDeleted = r.IsDeleted,
                                IngredientRecipeTypeId = r.IngredientRecipeTypeId,
                                IsInMenu = r.IsInMenu,
                                RecipeTypeId = r.RecipeTypeId,
                                PreparationTime = r.PreparationTime,
                                RecipeType = r.RecipeType,
                                IngredientSet = r.IngredientSet,
                                Votes = r.Votes,

                                TasteRate = r.Votes.Where(v => v.Type == VoteTypeEnm.Taste
                                && v.UserId == currentUserUId).ToList().Count > 0 ?
                   (int)r.Votes.Where(v => v.Type == VoteTypeEnm.Taste
                   && v.UserId == currentUserUId).First<Vote>().Value : 0,

                                EasyRate = r.Votes.Where(v => v.Type == VoteTypeEnm.Easy
                                && v.UserId == currentUserUId).ToList().Count > 0 ?
                   (int)r.Votes.Where(v => v.Type == VoteTypeEnm.Easy
                   && v.UserId == currentUserUId).First<Vote>().Value : 0,
                            });
        }

        private IQueryable<Recipe> GetByNames(string inputList)
        {
            var inputArray = inputList.Split(new char[] { ',', ' ' }, System.StringSplitOptions.RemoveEmptyEntries).ToList();
            string currentUserUId = this.httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var query = this.recipeRepository.All();
            foreach (string input in inputArray)
            {
                query = query
                .Where(r => r.Name.ToUpper().Contains(input.ToUpper()))
                .OrderBy(r => r.Name)
               .Select(r => new Recipe
               {
                   Id = r.Id,
                   Name = r.Name,
                   Text = r.Text,
                   ImagePath = r.ImagePath,
                   IngredientSetId = r.IngredientSetId,
                   LastCooked = r.LastCooked,
                   DeletedOn = r.DeletedOn,
                   ModifiedOn = r.ModifiedOn,
                   CreatedOn = r.CreatedOn,
                   IsDeleted = r.IsDeleted,
                   IngredientRecipeTypeId = r.IngredientRecipeTypeId,
                   IsInMenu = r.IsInMenu,
                   RecipeTypeId = r.RecipeTypeId,
                   PreparationTime = r.PreparationTime,
                   RecipeType = r.RecipeType,
                   IngredientSet = r.IngredientSet,
                   Votes = r.Votes,

                   TasteRate = r.Votes.Where(v => v.Type == VoteTypeEnm.Taste
                   && v.UserId == currentUserUId).ToList().Count > 0 ?
                   (int)r.Votes.Where(v => v.Type == VoteTypeEnm.Taste
                   && v.UserId == currentUserUId).First<Vote>().Value : 0,

                   EasyRate = r.Votes.Where(v => v.Type == VoteTypeEnm.Easy
                   && v.UserId == currentUserUId).ToList().Count > 0 ?
                   (int)r.Votes.Where(v => v.Type == VoteTypeEnm.Easy
                   && v.UserId == currentUserUId).First<Vote>().Value : 0,
               });
            }

            return query;
        }

        private IEnumerable<T> GetByRecipeTypeIds<T>(string inputList)
        {
            List<string> inputArray = new List<string>();
            var result = new List<T>();

            if (!string.IsNullOrEmpty(inputList))
            {
                var recipes = this.GetByRecipeTypeIds(inputList);
                result = recipes.To<T>().ToList();
            }

            return result;
        }
    }
}
