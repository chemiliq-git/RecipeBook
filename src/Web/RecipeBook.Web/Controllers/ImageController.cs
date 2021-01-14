namespace RecipeBook.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using RecipeBook.Web.ViewModels.Recipe;
    using System;
    using System.IO;

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
            if (!string.IsNullOrEmpty(data.Image))
            {
                var imageBase64Str = data.Image.Split(',')[1];
                if (!string.IsNullOrEmpty(imageBase64Str))
                {
                    byte[] contents = Convert.FromBase64String(imageBase64Str);
                    string path = "wwwroot/images/";

                    if (data.Type == "Recipes")
                    {
                        path += "Recipes";
                    }
                    else if (data.Type == "Products")
                    {
                        path += "Products";
                    }

                    string fileName = data.LinkedId + "_Img" + ".png";
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
