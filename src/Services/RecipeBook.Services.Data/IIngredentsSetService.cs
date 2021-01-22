namespace RecipeBook.Services.Data
{
    using System.Collections.Generic;

    public interface IIngredientsSetService
    {
        IEnumerable<T> GetAll<T>();

        IEnumerable<T> GetByRecipeId<T>(string input);


    }
}
