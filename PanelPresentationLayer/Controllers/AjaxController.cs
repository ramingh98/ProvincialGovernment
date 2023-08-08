using Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using PanelBusinessLogicLayer.BusinessComponents.IdentitiesComponents;
using PanelBusinessLogicLayer.BusinessServices.IdentitiesServices;
using PanelViewModel.IdentitiesViewModels;
using System.IO;

namespace PanelPresentationLayer.Controllers
{
    [Authorize]
    public class AjaxController : Controller
    {
        private readonly PersonnelService _personnelService;

        public AjaxController()
        {
            _personnelService = new PersonnelService();
        }

        [Route("/GetUsers")]
        public async Task<IActionResult> GetUsers([FromQuery] PersonnelFilterParams personnelFilterParams)
        {
            return await Task.FromResult(ViewComponent("Personnel", personnelFilterParams));
        }

        [Route("/LogOut")]
        public IActionResult LogOut()
        {
            HttpContext.Response.Cookies.Delete("code-token");
            return RedirectToPage("/Authentication/Login");
        }

        [Route("/AddExcel")]
        public async Task<OperationResult> AddExcel(IFormFile filee)
        {
            try
            {
                var stream = new MemoryStream();
                await filee.CopyToAsync(stream);
                using (ExcelPackage package = new ExcelPackage(stream))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.FirstOrDefault();
                    if (worksheet == null)
                    {
                        return new OperationResult();
                    }
                    else
                    {
                        //read excel file data and add data in  model.StaffInfoViewModel.StaffList
                        var rowCount = worksheet.Dimension.Rows;
                        List<PersonnelViewModel> cardExcelsList = new List<PersonnelViewModel>();
                        for (int row = 2; row <= rowCount; row++)
                        {
                            cardExcelsList.Add(new PersonnelViewModel
                            {
                                CaseNumber = Convert.ToInt64((worksheet.Cells[row, 1].Value ?? string.Empty).ToString().Trim()),
                                ComputerCode = (worksheet.Cells[row, 2].Value ?? string.Empty).ToString().Trim(),
                                Name = (worksheet.Cells[row, 3].Value ?? string.Empty).ToString().Trim(),
                                Family = (worksheet.Cells[row, 4].Value ?? string.Empty).ToString().Trim(),
                                FatherName = (worksheet.Cells[row, 5].Value ?? string.Empty).ToString().Trim(),
                                NationalCode = (worksheet.Cells[row, 6].Value ?? string.Empty).ToString().Trim(),
                                BirthCertificateNumber = (worksheet.Cells[row, 7].Value ?? string.Empty).ToString().Trim(),
                                BirthDate = (worksheet.Cells[row, 8].Value ?? string.Empty).ToString().Trim(),
                                PlaceOfBirth = (worksheet.Cells[row, 9].Value ?? string.Empty).ToString().Trim(),
                                TypeOfEmploymentId = Convert.ToInt64((worksheet.Cells[row, 10].Value ?? string.Empty).ToString().Trim()),
                                EducationDegreeId = Convert.ToInt64((worksheet.Cells[row, 11].Value ?? string.Empty).ToString().Trim()),
                                StudyFieldId = Convert.ToInt64((worksheet.Cells[row, 12].Value ?? string.Empty).ToString().Trim()),
                                LastPositionId = Convert.ToInt64((worksheet.Cells[row, 13].Value ?? string.Empty).ToString().Trim()),
                                ServiceLocationId = Convert.ToInt64((worksheet.Cells[row, 14].Value ?? string.Empty).ToString().Trim()),
                                MaritalStatusId = Convert.ToInt64((worksheet.Cells[row, 15].Value ?? string.Empty).ToString().Trim()),
                                CaseStatusId = Convert.ToInt64((worksheet.Cells[row, 16].Value ?? string.Empty).ToString().Trim()),
                            });
                        }
                        var result = await _personnelService.AddExcelAsync(cardExcelsList);
                        return new OperationResult() { Message = result.Message, Status = result.Status };
                    }
                }
            }
            catch (Exception ex)
            {
                return new OperationResult();
            }

        }
    }
}
