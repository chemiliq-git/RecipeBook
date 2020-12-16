using System;
using System.Collections.Generic;
using System.Text;

namespace RecipeBook.Services.Data
{
    public interface IRecipeTypeService
    {
        IEnumerable<T> GetAll<T>();
    }
}
