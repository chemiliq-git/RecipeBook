namespace RecipeBook.Web.Controllers
{
    using System;
    using System.IO;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using RecipeBook.Web.ViewModels.Common;

    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : Controller
    {
        public ImageController()
        {
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]

        // [Route("Upload")]
        public IActionResult Post([FromForm] ImageDataModel data)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest();
            }

            if (data != null && !string.IsNullOrEmpty(data.Image))
            {
                var imageBase64Str = data.Image.Split(',')[1];
                if (!string.IsNullOrEmpty(imageBase64Str))
                {
                    byte[] contents = Convert.FromBase64String(imageBase64Str);
                    string path = "wwwroot/images/";

                    if (data.Type == ImageDataTypeEnum.Recipe.ToString())
                    {
                        path += ImageDataTypeEnum.Recipe.ToString() + "s";
                    }
                    else if (data.Type == ImageDataTypeEnum.Ingredient.ToString())
                    {
                        path += ImageDataTypeEnum.Ingredient.ToString() + "s";
                    }

                    string fileName = data.LinkedId + "_Img" + ".jpg";
                    path = Path.Combine(path, fileName);

                    System.IO.File.WriteAllBytes(path, contents);

                    // remove "wwwroot"from path
                    path = path.Substring(7);
                    return this.Ok(path);
                }
            }

            return this.BadRequest();
        }

    }
}
