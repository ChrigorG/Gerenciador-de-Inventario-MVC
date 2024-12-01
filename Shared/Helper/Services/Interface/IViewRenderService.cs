using Microsoft.AspNetCore.Mvc;

namespace Shared.Helper.Services.Interface
{
    public interface IViewRenderService
    {
        Task<string> RenderToStringAsync(Controller controller, string viewName, object viewModel);
    }
}
