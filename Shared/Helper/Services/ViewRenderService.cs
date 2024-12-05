using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc;
using Shared.Helper.Services.Interface;

namespace Helper.Infra
{
    public class ViewRenderService : IViewRenderService
    {
        private readonly ICompositeViewEngine _compositeViewEngine;

        public ViewRenderService(ICompositeViewEngine compositeViewEngine)
        {
            _compositeViewEngine = compositeViewEngine;
        }

        public string RenderToString(Controller controller, string viewName, object viewModel)
        {
            controller.ViewData.Model = viewModel;

            using (var writer = new StringWriter())
            {
                var viewResult = _compositeViewEngine.FindView(controller.ControllerContext, viewName, false);

                if (!viewResult.Success)
                {
                    throw new FileNotFoundException($"A view '{viewName}' não foi encontrada.");
                }

                var viewContext = new ViewContext(
                    controller.ControllerContext,
                    viewResult.View,
                    controller.ViewData,
                    controller.TempData,
                    writer,
                    new HtmlHelperOptions()
                );

                _ = viewResult.View.RenderAsync(viewContext);
                return writer.GetStringBuilder().ToString();
            }
        }
    }
}
