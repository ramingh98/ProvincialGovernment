using Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PanelBusinessLogicLayer.BusinessServices.BaseDefinitionsServices;
using PanelPresentationLayer.Infrastructure.RazorUtils;
using PanelViewModel.BaseDefinitionsViewModels;

namespace PanelPresentationLayer.Pages.Panel.BaseDefinitions.EmploymentTypes
{
    [Authorize]
    public class IndexModel : BaseRazor
    {
        private readonly EmploymentTypeService _employmentService;
        private readonly IRenderViewToString _renderViewToString;

        public IndexModel(IRenderViewToString renderViewToString)
        {
            _employmentService = new EmploymentTypeService();
            _renderViewToString = renderViewToString;
        }

        public List<EmploymentTypeViewModel> EmploymentTypeViewModel { get; set; }

        public async Task OnGet()
        {
            EmploymentTypeViewModel = await _employmentService.ReadAsync();
        }

        public async Task<IActionResult> OnGetRenderAddPage()
        {
            return await AjaxTryCatch(async () =>
            {
                var view = await _renderViewToString.RenderToStringAsync("_Add", new EmploymentTypeViewModel(), PageContext);
                return OperationResult<string>.Success(view);
            });
        }

        public async Task<IActionResult> OnPostAddEmploymentType(EmploymentTypeViewModel viewModel)
        {
            return await AjaxTryCatch(async () =>
            {
                var result = await _employmentService.AddAsync(viewModel);
                return result;
            }, true);
        }

        public async Task<IActionResult> OnGetRenderEditPage(long id)
        {
            var model = await _employmentService.FindAsync(id);
            return await AjaxTryCatch(async () =>
            {
                var view = await _renderViewToString.RenderToStringAsync("_Edit", new EmploymentTypeViewModel()
                {
                    Id = model.Value.Id,
                    Title = model.Value.Title
                }, PageContext);
                return OperationResult<string>.Success(view);
            });
        }

        public async Task<IActionResult> OnPostEditType(EmploymentTypeViewModel viewModel)
        {
            return await AjaxTryCatch(async () =>
            {
                var result = await _employmentService.UpdateAsync(viewModel);
                return result;
            }, true);
        }

        public async Task<IActionResult> OnPostDeleteType(long id)
        {
            return await AjaxTryCatch(async () =>
            {
                return await _employmentService.DeleteAsync(id);
            });
        }
    }
}
