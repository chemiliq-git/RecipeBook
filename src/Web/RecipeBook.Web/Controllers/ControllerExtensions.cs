﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;

namespace RecipeBook.Web.Controllers
{
    public static class ControllerExtensions
    {
        public static async Task<string> RenderViewToStringAsync(this Controller controller, string viewNamePath, object model = null)
        {
            if (string.IsNullOrEmpty(viewNamePath))
                viewNamePath = controller.ControllerContext.ActionDescriptor.ActionName;

            controller.ViewData.Model = model;

            using (StringWriter writer = new StringWriter())
            {
                try
                {
                    var view = FindView(controller, viewNamePath);

                    ViewContext viewContext = new ViewContext(
                        controller.ControllerContext,
                        view,
                        controller.ViewData,
                        controller.TempData,
                        writer,
                        new HtmlHelperOptions()
                    );

                    await view.RenderAsync(viewContext);

                    return writer.GetStringBuilder().ToString();
                }
                catch (Exception exc)
                {
                    return $"Failed - {exc.Message}";
                }
            }
        }

        private static IView FindView(Controller controller, string viewNamePath)
        {
            IViewEngine viewEngine = controller.HttpContext.RequestServices.GetService(typeof(ICompositeViewEngine)) as ICompositeViewEngine;

            ViewEngineResult viewResult = viewEngine.FindView(controller.ControllerContext, viewNamePath, false);

            if (!viewResult.Success)
            {
                var endPointDisplay = controller.HttpContext.GetEndpoint().DisplayName;

                if (endPointDisplay.Contains(".Areas."))
                {
                    //search in Areas
                    var areaName = endPointDisplay.Substring(endPointDisplay.IndexOf(".Areas.") + ".Areas.".Length);
                    areaName = areaName.Substring(0, areaName.IndexOf(".Controllers."));

                    viewNamePath = $"~/Areas/{areaName}/views/{controller.HttpContext.Request.RouteValues["controller"]}/{controller.HttpContext.Request.RouteValues["action"]}.cshtml";

                    viewResult = viewEngine.GetView(viewNamePath, viewNamePath, false);
                }

                if (!viewResult.Success)
                    throw new Exception($"A view with the name '{viewNamePath}' could not be found");

            }

            return viewResult.View;
        }
    }
}
