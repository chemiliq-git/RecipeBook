using System;
using System.Collections.Generic;
using System.Text;

namespace RecipeBook.Services.Data
{
    public interface IRecipeTypeService
    {
        List<T> GetAll<T>();
    }
}
