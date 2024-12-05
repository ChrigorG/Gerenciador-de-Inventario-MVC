using Microsoft.AspNetCore.Mvc;

namespace Shared.Helper.Services.Interface
{
    public interface IViewRenderService
    {
        string RenderToString(Controller controller, string viewName, object viewModel);
    }
}
