using Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PanelBusinessLogicLayer.BusinessServices.BaseDefinitionsServices;
using PanelPresentationLayer.Infrastructure.RazorUtils;
using PanelViewModel.BaseDefinitionsViewModels;

namespace PanelPresentationLayer.Pages.Panel.BaseDefinitions.ServiceLocation
{
    [Authorize]
    public class IndexModel : BaseRazor
    {
        private readonly ServiceLocationService _serviceLocationService;
        private readonly IRenderViewToString _renderViewToString;

        public IndexModel(IRenderViewToString renderViewToString)
        {
            _serviceLocationService = new ServiceLocationService();
            _renderViewToString = renderViewToString;
        }

        public List<ServiceLocationViewModel> ServiceLocationViewModel { get; set; }

        public async Task OnGet()
        {
            ServiceLocationViewModel = await _serviceLocationService.ReadAsync();
        }

        public async Task<IActionResult> OnGetRenderAddPage()
        {
            return await AjaxTryCatch(async () =>
            {
                var view = await _renderViewToString.RenderToStringAsync("_Add", new ServiceLocationViewModel(), PageContext);
                return OperationResult<string>.Success(view);
            });
        }

        public async Task<IActionResult> OnPostAddPosition(ServiceLocationViewModel viewModel)
        {
            return await AjaxTryCatch(async () =>
            {
                var result = await _serviceLocationService.AddAsync(viewModel);
                return result;
            }, true);
        }

        public async Task<IActionResult> OnGetRenderEditPage(long id)
        {
            var model = await _serviceLocationService.FindAsync(id);
            return await AjaxTryCatch(async () =>
            {
                var view = await _renderViewToString.RenderToStringAsync("_Edit", new ServiceLocationViewModel()
                {
                    Id = model.Id,
                    Title = model.Title
                }, PageContext);
                return OperationResult<string>.Success(view);
            });
        }

        public async Task<IActionResult> OnPostEditPosition(ServiceLocationViewModel viewModel)
        {
            return await AjaxTryCatch(async () =>
            {
                var result = await _serviceLocationService.UpdateAsync(viewModel);
                return result;
            }, true);
        }

        public async Task<IActionResult> OnPostDeleteType(long id)
        {
            return await AjaxTryCatch(async () =>
            {
                return await _serviceLocationService.DeleteAsync(id);
            });
        }
    }
}
