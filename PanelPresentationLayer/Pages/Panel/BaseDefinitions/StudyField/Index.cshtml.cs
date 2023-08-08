using Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PanelBusinessLogicLayer.BusinessServices.BaseDefinitionsServices;
using PanelPresentationLayer.Infrastructure.RazorUtils;
using PanelViewModel.BaseDefinitionsViewModels;

namespace PanelPresentationLayer.Pages.Panel.BaseDefinitions.StudyField
{
    [Authorize]
    public class IndexModel : BaseRazor
    {
        private readonly StudyFieldService _studyFieldService;
        private readonly IRenderViewToString _renderViewToString;

        public IndexModel(IRenderViewToString renderViewToString)
        {
            _studyFieldService = new StudyFieldService();
            _renderViewToString = renderViewToString;
        }

        public List<StudyFieldViewModel> StudyFieldViewModels { get; set; }

        public async Task OnGet()
        {
            StudyFieldViewModels = await _studyFieldService.ReadAsync();
        }

        public async Task<IActionResult> OnGetRenderAddPage()
        {
            return await AjaxTryCatch(async () =>
            {
                var view = await _renderViewToString.RenderToStringAsync("_Add", new StudyFieldViewModel(), PageContext);
                return OperationResult<string>.Success(view);
            });
        }

        public async Task<IActionResult> OnPostAddField(StudyFieldViewModel viewModel)
        {
            return await AjaxTryCatch(async () =>
            {
                var result = await _studyFieldService.AddAsync(viewModel);
                return result;
            }, true);
        }

        public async Task<IActionResult> OnGetRenderEditPage(long id)
        {
            var model = await _studyFieldService.FindAsync(id);
            return await AjaxTryCatch(async () =>
            {
                var view = await _renderViewToString.RenderToStringAsync("_Edit", new StudyFieldViewModel()
                {
                    Id = model.Value.Id,
                    Title = model.Value.Title
                }, PageContext);
                return OperationResult<string>.Success(view);
            });
        }

        public async Task<IActionResult> OnPostEditField(StudyFieldViewModel viewModel)
        {
            return await AjaxTryCatch(async () =>
            {
                var result = await _studyFieldService.UpdateAsync(viewModel);
                return result;
            }, true);
        }

        public async Task<IActionResult> OnPostDeleteField(long id)
        {
            return await AjaxTryCatch(async () =>
            {
                return await _studyFieldService.DeleteAsync(id);
            });
        }
    }
}
