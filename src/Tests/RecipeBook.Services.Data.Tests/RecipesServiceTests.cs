namespace RecipeBook.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Security.Claims;
    using System.Security.Principal;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using RecipeBook.Data;
    using RecipeBook.Data.Common.Repositories;
    using RecipeBook.Data.Models;
    using RecipeBook.Data.Repositories;
    using RecipeBook.Services.Mapping;
    using Xunit;

    public class RecipesServiceTests
    {
        private const string FIRST_TEST_RECIPE_UID = "FIRST_TEST_RECIPE_UID";
        private const string SECOND_TEST_RECIPE_UID = "SECOND_TEST_RECIPE_UID";
        private const string THIRD_TEST_RECIPE_UID = "THIRD_TEST_RECIPE_UID";

        private const string FIRST_TEST_RECIPE_NAME_BEAN_SOUP = "Bean soup";
        private const string SECOND_TEST_RECIPE_NAME_LETILS_SOUP = "Letils soup";
        private const string THIRD_TEST_RECIPE_NAME_GOULASH_SOUP = "Goulash soup";

        private const string TEST_RECIPE_NAME_BEAN = "Bean";
        private const string TEST_RECIPE_NAME_LETILS = "Letils";
        private const string TEST_RECIPE_NAME_SOUP = "Soup";

        private const string FIRST_USER_UID = "FIRST_USER_UID";
        private const string SECOND_USER_UID = "SECOND_USER_UID";
        private const string FIRST_USER_NAME = "Madelena Max";

        private const string FIRST_VOTE_UID = "FIRST_VOTE_UID";
        private const string SECOND_VOTE_UID = "SECOND_VOTE_UID";
        private const string THIRD_VOTE_UID = "THIRD_VOTE_UID";

        private const string RECIPE_TYPE_NAME_DESSERTS = "Desserts";
        private const string RECIPE_TYPE_NAME_MAINDISHES = "Main dishes";
        private const string RECIPE_TYPE_UID_DESSERTS = "RECIPE_TYPE_DESSERTS_UID";
        private const string RECIPE_TYPE_UID_MAINDISHES = "RECIPE_TYPE_MAINDISHES_UID";

        private const string FIRST_INGREDIENTSET_UID = "FIRST_INGREDIENTSET_UID";
        private const string SECOND_INGREDIENTSET_UID = "SECOND_INGREDIENTSET_UID";
        private const string THIRD_INGREDIENTSET_UID = "FIRST_INGREDIENTSET_UID";

        private const string FIRST_INGREDIENTSET_ITEM_UID = "FIRST_INGREDIENTSET_ITEM_UID";
        private const string SECOND_INGREDIENTSET_ITEM_UID = "SECOND_INGREDIENTSET_ITEM_UID";
        private const string THIRD_INGREDIENTSET_ITEM_UID = "THIRD_INGREDIENTSET_ITEM_UID";
        private const string FOURTH_INGREDIENTSET_ITEM_UID = "FOURTH_INGREDIENTSET_ITEM_UID";
        private const string FIFTH_INGREDIENTSET_ITEM_UID = "FIFTH_INGREDIENTSET_ITEM_UID";
        private const string SIXTH_INGREDIENTSET_ITEM_UID = "SIXTH_INGREDIENTSET_ITEM_UID";
        [Fact]
        public void GetAllShouldReturnCorrectNumber()
        {           
            var mockHttpContextAccessor = new MockHttpContext();
            var context = new DefaultHttpContext();
            mockHttpContextAccessor.SetContextUser(FIRST_USER_NAME, FIRST_USER_UID, ref context);
            mockHttpContextAccessor.SetupMockContext(ref context);
            //// Mock Vote Repository
            //var mockFirstRecipeVoteRepository = new Mock<IRepository<Vote>>();
            //mockFirstRecipeVoteRepository.Setup(r => r.All()).Returns(new List<Vote>
            //                                            {
            //                                                new Vote()
            //                                                { Id = FIRST_VOTE_UID,
            //                                                  UserId = FIRST_USER_UID,
            //                                                  RecipeId = FIRST_TEST_RECIPE_UID,
            //                                                  Type = VoteTypeEnm.Easy,
            //                                                  Value = 4,
            //                                                },
            //                                                new Vote()
            //                                                { Id = SECOND_VOTE_UID,
            //                                                  UserId = SECOND_USER_UID,
            //                                                  RecipeId = FIRST_TEST_RECIPE_UID,
            //                                                  Type = VoteTypeEnm.Easy,
            //                                                  Value = 5,
            //                                                },
            //                                                new Vote()
            //                                                { Id = THIRD_VOTE_UID,
            //                                                  UserId = SECOND_USER_UID,
            //                                                  RecipeId = FIRST_TEST_RECIPE_UID,
            //                                                  Type = VoteTypeEnm.Taste,
            //                                                  Value = 1,
            //                                                },
            //                                            }.AsQueryable());

            // Mock Recipe Repository
            var mockRecipeRepository = new Mock<IDeletableEntityRepository<Recipe>>();
            mockRecipeRepository.Setup(r => r.All()).Returns(new List<Recipe>
                                                        {
                                                            new Recipe()
                                                            {
                                                                Name = FIRST_TEST_RECIPE_NAME_BEAN_SOUP,
                                                                Id = FIRST_TEST_RECIPE_UID,
                                                            },
                                                            new Recipe()
                                                            {
                                                                Name = SECOND_TEST_RECIPE_NAME_LETILS_SOUP,
                                                                Id = SECOND_TEST_RECIPE_UID,
                                                            },
                                                            new Recipe()
                                                            {
                                                                Name = THIRD_TEST_RECIPE_NAME_GOULASH_SOUP,
                                                                Id = THIRD_TEST_RECIPE_UID,
                                                            },
                                                        }.AsQueryable());


            var service = new RecipeService(mockRecipeRepository.Object, mockHttpContextAccessor.Object);
            AutoMapperConfig.RegisterMappings(typeof(TestRecipe).GetTypeInfo().Assembly);
            var result = service.GetAll<TestRecipe>();
            Assert.Equal(3, result.Count());
            mockRecipeRepository.Verify();
        }

        [Fact]
        public void GetByNamesShortShouldReturnCorrectName()
        {
            var mockHttpContextAccessor = new MockHttpContext();
            var context = new DefaultHttpContext();
            mockHttpContextAccessor.SetContextUser(FIRST_USER_NAME, FIRST_USER_UID, ref context);
            mockHttpContextAccessor.SetupMockContext(ref context);

            var repository = new Mock<IDeletableEntityRepository<Recipe>>();
            repository.Setup(r => r.All()).Returns(new List<Recipe>
                                                        {
                                                             new Recipe()
                                                            {
                                                                Name = FIRST_TEST_RECIPE_NAME_BEAN_SOUP,
                                                                Id = FIRST_TEST_RECIPE_UID,
                                                            },
                                                             new Recipe()
                                                            {
                                                                Name = SECOND_TEST_RECIPE_NAME_LETILS_SOUP,
                                                                Id = SECOND_TEST_RECIPE_UID,
                                                            },
                                                             new Recipe()
                                                            {
                                                                Name = THIRD_TEST_RECIPE_NAME_GOULASH_SOUP,
                                                                Id = THIRD_TEST_RECIPE_UID,
                                                            },
                                                        }.AsQueryable());

            var service = new RecipeService(repository.Object, mockHttpContextAccessor.Object);
            AutoMapperConfig.RegisterMappings(typeof(TestRecipe).GetTypeInfo().Assembly);
            var result = service.GetByNames<TestRecipe>(TEST_RECIPE_NAME_LETILS + "," + TEST_RECIPE_NAME_SOUP);
            Assert.Equal(SECOND_TEST_RECIPE_NAME_LETILS_SOUP, result.ToList()[0].Name);
            repository.Verify();
        }

        [Fact]
        public void GetByNamesFullShouldReturnCorrectName()
        {
            var mockHttpContextAccessor = new MockHttpContext();
            var context = new DefaultHttpContext();
            mockHttpContextAccessor.SetContextUser(FIRST_USER_NAME, FIRST_USER_UID, ref context);
            mockHttpContextAccessor.SetupMockContext(ref context);

            var repository = new Mock<IDeletableEntityRepository<Recipe>>();
            repository.Setup(r => r.All()).Returns(new List<Recipe>
                                                        {
                                                             new Recipe()
                                                            {
                                                                Name = FIRST_TEST_RECIPE_NAME_BEAN_SOUP,
                                                                Id = FIRST_TEST_RECIPE_UID,
                                                            },
                                                             new Recipe()
                                                            {
                                                                Name = SECOND_TEST_RECIPE_NAME_LETILS_SOUP,
                                                                Id = SECOND_TEST_RECIPE_UID,
                                                            },
                                                             new Recipe()
                                                            {
                                                                Name = THIRD_TEST_RECIPE_NAME_GOULASH_SOUP,
                                                                Id = THIRD_TEST_RECIPE_UID,
                                                            },
                                                        }.AsQueryable());

            var service = new RecipeService(repository.Object, mockHttpContextAccessor.Object);
            AutoMapperConfig.RegisterMappings(typeof(TestRecipe).GetTypeInfo().Assembly);
            var result = service.GetByNames<TestRecipe>(FIRST_TEST_RECIPE_NAME_BEAN_SOUP);
            Assert.Equal(FIRST_TEST_RECIPE_NAME_BEAN_SOUP, result.ToList()[0].Name);
            repository.Verify();
        }

        [Fact]
        public void GetByIsInMenuShouldReturnCorrectNumber()
        {
            var mockHttpContextAccessor = new MockHttpContext();
            var context = new DefaultHttpContext();
            mockHttpContextAccessor.SetContextUser(FIRST_USER_NAME, FIRST_USER_UID, ref context);
            mockHttpContextAccessor.SetupMockContext(ref context);

            var repository = new Mock<IDeletableEntityRepository<Recipe>>();
            repository.Setup(r => r.All()).Returns(new List<Recipe>
                                                        {
                                                            new Recipe()
                                                            {
                                                                Name = FIRST_TEST_RECIPE_NAME_BEAN_SOUP,
                                                                Id = FIRST_TEST_RECIPE_UID,
                                                                IsInMenu = true,
                                                            },
                                                            new Recipe()
                                                            {
                                                                Name = SECOND_TEST_RECIPE_NAME_LETILS_SOUP,
                                                                Id = SECOND_TEST_RECIPE_UID,
                                                                IsInMenu = true,
                                                            },
                                                            new Recipe()
                                                            {
                                                                Name = THIRD_TEST_RECIPE_NAME_GOULASH_SOUP,
                                                                Id = THIRD_TEST_RECIPE_UID,
                                                            },
                                                        }.AsQueryable());

            var service = new RecipeService(repository.Object, mockHttpContextAccessor.Object);
            AutoMapperConfig.RegisterMappings(typeof(TestRecipe).GetTypeInfo().Assembly);
            var result = service.GetByIsInMenu<TestRecipe>();
            Assert.Equal(2, result.Count());
            repository.Verify();
        }

        [Fact]
        public void GetByIdShouldReturnCorrectRecipeId()
        {
            var mockHttpContextAccessor = new MockHttpContext();
            var context = new DefaultHttpContext();
            mockHttpContextAccessor.SetContextUser(FIRST_USER_NAME, FIRST_USER_UID, ref context);
            mockHttpContextAccessor.SetupMockContext(ref context);

            var repository = new Mock<IDeletableEntityRepository<Recipe>>();
            repository.Setup(r => r.All()).Returns(new List<Recipe>
                                                        {
                                                            new Recipe()
                                                            {
                                                                Name = FIRST_TEST_RECIPE_NAME_BEAN_SOUP,
                                                                Id = FIRST_TEST_RECIPE_UID,
                                                            },
                                                            new Recipe()
                                                            {
                                                                Name = SECOND_TEST_RECIPE_NAME_LETILS_SOUP,
                                                                Id = SECOND_TEST_RECIPE_UID,
                                                            },
                                                            new Recipe()
                                                            {
                                                                Name = THIRD_TEST_RECIPE_NAME_GOULASH_SOUP,
                                                                Id = THIRD_TEST_RECIPE_UID,
                                                            },
                                                        }.AsQueryable());

            var service = new RecipeService(repository.Object, mockHttpContextAccessor.Object);
            AutoMapperConfig.RegisterMappings(typeof(TestRecipe).GetTypeInfo().Assembly);
            var result = service.GetById<TestRecipe>(FIRST_TEST_RECIPE_UID);
            Assert.Equal(FIRST_TEST_RECIPE_UID, result.Id);
            repository.Verify();
        }

        [Fact]
        public void GetFromSearchDataByRecipeNameShouldReturnCorrectRecipe()
        {
            var mockHttpContextAccessor = new MockHttpContext();
            var context = new DefaultHttpContext();
            mockHttpContextAccessor.SetContextUser(FIRST_USER_NAME, FIRST_USER_UID, ref context);
            mockHttpContextAccessor.SetupMockContext(ref context);

            //var mockRecipeType = new Mock<IDeletableEntityRepository<RecipeType>>();
            //mockRecipeType.Setup(rt => rt.All()).Returns(new List<RecipeType>()
            //                                            {
            //                                                new RecipeType()
            //                                                {
            //                                                    Id = RECIPE_TYPE_UID_DESSERTS,
            //                                                    Name = RECIPE_TYPE_NAME_DESSERTS,
            //                                                },
            //                                                new RecipeType()
            //                                                {
            //                                                    Id=RECIPE_TYPE_UID_MAINDISHES,
            //                                                    Name = RECIPE_TYPE_NAME_MAINDISHES,
            //                                                },
            //                                            }.AsQueryable());

            var mockRecipeRepository = new Mock<IDeletableEntityRepository<Recipe>>();
            mockRecipeRepository.Setup(r => r.All()).Returns(new List<Recipe>
                                                        {
                                                            new Recipe()
                                                            {
                                                                Name = FIRST_TEST_RECIPE_NAME_BEAN_SOUP,
                                                                Id = FIRST_TEST_RECIPE_UID,
                                                                RecipeType = new RecipeType()
                                                                {
                                                                    Id=RECIPE_TYPE_UID_MAINDISHES,
                                                                    Name = RECIPE_TYPE_NAME_MAINDISHES,
                                                                },
                                                            },
                                                            new Recipe()
                                                            {
                                                                Name = SECOND_TEST_RECIPE_NAME_LETILS_SOUP,
                                                                Id = SECOND_TEST_RECIPE_UID,
                                                                RecipeType = new RecipeType()
                                                                {
                                                                    Id=RECIPE_TYPE_UID_MAINDISHES,
                                                                    Name = RECIPE_TYPE_NAME_MAINDISHES,
                                                                },
                                                            },
                                                            new Recipe()
                                                            {
                                                                Name = THIRD_TEST_RECIPE_NAME_GOULASH_SOUP,
                                                                Id = THIRD_TEST_RECIPE_UID,
                                                                RecipeType = new RecipeType()
                                                                {
                                                                    Id=RECIPE_TYPE_UID_MAINDISHES,
                                                                    Name = RECIPE_TYPE_NAME_MAINDISHES,
                                                                },
                                                            },
                                                        }.AsQueryable());

            var service = new RecipeService(mockRecipeRepository.Object, mockHttpContextAccessor.Object);
            AutoMapperConfig.RegisterMappings(typeof(TestRecipe).GetTypeInfo().Assembly);
            var result = service.GetFromHomeSearchData<TestRecipe>(FIRST_TEST_RECIPE_NAME_BEAN_SOUP, string.Empty);
            Assert.Equal(FIRST_TEST_RECIPE_UID, result.ToList()[0].Id);
            mockRecipeRepository.Verify();
        }
        [Fact]
        public void GetFromSearchDataByRecipeTypeShouldReturnCorrectRecipes()
        {
            var mockHttpContextAccessor = new MockHttpContext();
            var context = new DefaultHttpContext();
            mockHttpContextAccessor.SetContextUser(FIRST_USER_NAME, FIRST_USER_UID, ref context);
            mockHttpContextAccessor.SetupMockContext(ref context);
                        

            var mockRecipeRepository = new Mock<IDeletableEntityRepository<Recipe>>();
            mockRecipeRepository.Setup(r => r.All()).Returns(new List<Recipe>
                                                        {
                                                            new Recipe()
                                                            {
                                                                Name = FIRST_TEST_RECIPE_NAME_BEAN_SOUP,
                                                                Id = FIRST_TEST_RECIPE_UID,
                                                                RecipeTypeId = RECIPE_TYPE_UID_MAINDISHES,
                                                            },
                                                            new Recipe()
                                                            {
                                                                Name = SECOND_TEST_RECIPE_NAME_LETILS_SOUP,
                                                                Id = SECOND_TEST_RECIPE_UID,
                                                                RecipeTypeId = RECIPE_TYPE_UID_MAINDISHES,
                                                            },
                                                            new Recipe()
                                                            {
                                                                Name = THIRD_TEST_RECIPE_NAME_GOULASH_SOUP,
                                                                Id = THIRD_TEST_RECIPE_UID,
                                                                RecipeTypeId = RECIPE_TYPE_UID_MAINDISHES,
                                                            },
                                                        }.AsQueryable());

            var service = new RecipeService(mockRecipeRepository.Object, mockHttpContextAccessor.Object);
            AutoMapperConfig.RegisterMappings(typeof(TestRecipe).GetTypeInfo().Assembly);
            var result = service.GetFromHomeSearchData<TestRecipe>( string.Empty, RECIPE_TYPE_UID_MAINDISHES);
            Assert.Equal(3, result.Count());
            mockRecipeRepository.Verify();
        }
        
        [Fact]
        public void GetByNamesAndRecipeTypeIdsAndIngrIdsReturnCorrectRecipes()
        {
            var mockHttpContextAccessor = new MockHttpContext();
            var context = new DefaultHttpContext();
            mockHttpContextAccessor.SetContextUser(FIRST_USER_NAME, FIRST_USER_UID, ref context);
            mockHttpContextAccessor.SetupMockContext(ref context);


            var mockRecipeRepository = new Mock<IDeletableEntityRepository<Recipe>>();
            mockRecipeRepository.Setup(r => r.All()).Returns(new List<Recipe>
                                                        {
                                                            new Recipe()
                                                            {
                                                                Name = FIRST_TEST_RECIPE_NAME_BEAN_SOUP,
                                                                Id = FIRST_TEST_RECIPE_UID,
                                                                RecipeTypeId = RECIPE_TYPE_UID_MAINDISHES,
                                                                IngredientSet  = new IngredientsSet()
                                                                {
                                                                    Id = FIRST_INGREDIENTSET_UID,
                                                                    IngredientSetItems = new List<IngredientsSetItem>()
                                                                    {
                                                                        new IngredientsSetItem()
                                                                        {
                                                                            Id="999",
                                                                            IngredientID  = "123"
                                                                        },
                                                                        new IngredientsSetItem()
                                                                        {
                                                                            Id="888",
                                                                            IngredientID  = "568"
                                                                        },
                                                                    },
                                                                },
                                                            },
                                                            new Recipe()
                                                            {
                                                                Name = SECOND_TEST_RECIPE_NAME_LETILS_SOUP,
                                                                Id = SECOND_TEST_RECIPE_UID,
                                                                RecipeTypeId = RECIPE_TYPE_UID_MAINDISHES,
                                                                IngredientSet = new IngredientsSet()
                                                                {
                                                                    Id = SECOND_INGREDIENTSET_UID,
                                                                    IngredientSetItems = new List<IngredientsSetItem>()
                                                                    {
                                                                        new IngredientsSetItem()
                                                                        {
                                                                            Id="56",
                                                                            IngredientID  = "123"
                                                                        },
                                                                        new IngredientsSetItem()
                                                                        {
                                                                            Id="65",
                                                                            IngredientID  = "568"
                                                                        },
                                                                    },
                                                                },
                                                            },
                                                            new Recipe()
                                                            {
                                                                Name = THIRD_TEST_RECIPE_NAME_GOULASH_SOUP,
                                                                Id = THIRD_TEST_RECIPE_UID,
                                                                RecipeTypeId = RECIPE_TYPE_UID_MAINDISHES,
                                                                IngredientSet  = new IngredientsSet()
                                                                {
                                                                    Id = THIRD_INGREDIENTSET_UID,
                                                                    IngredientSetItems = new List<IngredientsSetItem>()
                                                                    {
                                                                        new IngredientsSetItem()
                                                                        {
                                                                            Id="45",
                                                                            IngredientID  = "123"
                                                                        },
                                                                        new IngredientsSetItem()
                                                                        {
                                                                            Id="54",
                                                                            IngredientID  = "568"
                                                                        },
                                                                    },
                                                                },
                                                            },
                                                        }.AsQueryable());

            var service = new RecipeService(mockRecipeRepository.Object, mockHttpContextAccessor.Object);
            AutoMapperConfig.RegisterMappings(typeof(TestRecipe).GetTypeInfo().Assembly);

            var result = service.GetByNamesAndRecipeTypeIdsAndIngrIds<TestRecipe>(FIRST_TEST_RECIPE_NAME_BEAN_SOUP, RECIPE_TYPE_UID_MAINDISHES,
                "568,54");
            Assert.Equal(FIRST_TEST_RECIPE_UID, result.ToList()[0].Id);
            mockRecipeRepository.Verify();
        }

    }
}
