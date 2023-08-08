using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PanelBusinessLogicLayer.BusinessComponents.IdentitiesComponents;
using PanelBusinessLogicLayer.BusinessServices.IdentitiesServices;
using PanelViewModel.IdentitiesViewModels;
using System.Data;

namespace PanelPresentationLayer.ViewComponents.Personnels
{
    public class Personnel : ViewComponent
    {
        private readonly PersonnelService _personnelService;
        public Personnel()
        {
            _personnelService = new PersonnelService();
        }

        public async Task<IViewComponentResult> InvokeAsync(PersonnelFilterParams filterParams)
        {
            var result = await _personnelService.ReadAsync(filterParams);
            int skip = (filterParams.PageId - 1) * filterParams.Take;
            int Count = result.Data.Count();
            var pageCount = (Count / filterParams.Take) +1;
            ViewBag.PageID = filterParams.PageId;
            ViewBag.PageCount = pageCount;
            ViewBag.StartPage = filterParams.PageId - 4 <= 0 ? 1 : filterParams.PageId - 4;
            ViewBag.EndPage = filterParams.PageId + 5 > pageCount ? pageCount : filterParams.PageId + 5;
            var list = result.Data.Skip(skip).Take(filterParams.Take).ToList();
            return await Task.FromResult((IViewComponentResult)View("/Pages/Panel/Identities/Personnels/DynamicList.cshtml", list));
        }
    }
}
