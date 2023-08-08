using Common;
using Common.Objects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PanelBusinessLogicLayer.BusinessServices.BaseDefinitionsServices;
using PanelPresentationLayer.Infrastructure.RazorUtils;
using PanelViewModel.BaseDefinitionsViewModels;

namespace PanelPresentationLayer.Pages.Panel.BaseDefinitions.PositionType
{
    [Authorize]
    public class IndexModel : BaseRazor
    {
        private readonly PositionTypeService _positionTypeService;
        private readonly IRenderViewToString _renderViewToString;

        public IndexModel(IRenderViewToString renderViewToString)
        {
            _positionTypeService = new PositionTypeService();
            _renderViewToString = renderViewToString;
        }

        public SysResult<List<PositionTypeViewModel>> PositionTypeViewModel { get; set; }

        public async Task OnGet()
        {
            PositionTypeViewModel = await _positionTypeService.ReadAsync();
        }

        public async Task<IActionResult> OnGetRenderAddPage()
        {
            return await AjaxTryCatch(async () =>
            {
                var view = await _renderViewToString.RenderToStringAsync("_Add", new PositionTypeViewModel(), PageContext);
                return OperationResult<string>.Success(view);
            });
        }

        public async Task<IActionResult> OnPostAddPosition(PositionTypeViewModel viewModel)
        {
            return await AjaxTryCatch(async () =>
            {
                var result = await _positionTypeService.AddAsync(viewModel);
                return result;
            }, true);
        }

        public async Task<IActionResult> OnGetRenderEditPage(long id)
        {
            var model = await _positionTypeService.FindAsync(id);
            return await AjaxTryCatch(async () =>
            {
                var view = await _renderViewToString.RenderToStringAsync("_Edit", new PositionTypeViewModel()
                {
                    Id = model.Value.Id,
                    Title = model.Value.Title
                }, PageContext);
                return OperationResult<string>.Success(view);
            });
        }

        public async Task<IActionResult> OnPostEditPosition(PositionTypeViewModel viewModel)
        {
            return await AjaxTryCatch(async () =>
            {
                var result = await _positionTypeService.UpdateAsync(viewModel);
                return result;
            }, true);
        }

        public async Task<IActionResult> OnPostDeleteType(long id)
        {
            return await AjaxTryCatch(async () =>
            {
                return await _positionTypeService.DeleteAsync(id);
            });
        }
    }
}
