using Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using OfficeOpenXml;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using PanelBusinessLogicLayer.BusinessComponents.IdentitiesComponents;
using PanelBusinessLogicLayer.BusinessServices.IdentitiesServices;
using PanelPresentationLayer.Infrastructure.RazorUtils;
using PanelViewModel.Filters;
using PanelViewModel.IdentitiesViewModels;

namespace PanelPresentationLayer.Pages.Panel.Identities.Personnels
{
    [Authorize]
    public class IndexModel : BaseRazorFilter<PersonnelFilterParams>
    {
        private readonly PersonnelService _personnelService;
        private readonly IRenderViewToString _renderViewToString;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _environment;

        public IndexModel(IRenderViewToString renderViewToString, Microsoft.AspNetCore.Hosting.IHostingEnvironment environment)
        {
            _personnelService = new PersonnelService();
            _renderViewToString = renderViewToString;
            _environment = environment;
        }

        public PersonnelFilterResult FilterResult { get; set; }

        public async Task OnGet(PersonnelFilterParams personnelFilterParams)
        {
            //FilterResult = await _personnelService.ReadAsync(FilterParams);
            ViewData["FullName"] = personnelFilterParams.FullName;
            ViewData["NationalCode"] = personnelFilterParams.NationalCode;
            ViewData["EmploymentTypeId"] = personnelFilterParams.EmploymentTypeId;
            ViewData["CaseStatus"] = personnelFilterParams.CaseStatus;
            ViewData["ServiceLocation"] = personnelFilterParams.ServiceLocation;
            ViewData["CaseNumber"] = personnelFilterParams.CaseNumber;
            ViewData["EducationDegree"] = personnelFilterParams.EducationDegreeId;
            ViewData["PageId"] = personnelFilterParams.PageId;
            ViewData["Take"] = personnelFilterParams.Take;
            FilterResult = await _personnelService.ReadAsync(personnelFilterParams);
        }

        public IActionResult OnGetDownloadExcel()
        {
            var excelFilePath = Path.Combine(_environment.WebRootPath, "excel/sample.xlsx");

            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(excelFilePath, out var contentType))
            {
                contentType = "application/octet-stream";
            }

            var fileStream = new FileStream(excelFilePath, FileMode.Open);
            return PhysicalFile(excelFilePath, contentType, Path.GetFileName(excelFilePath));
        }

        public async Task<IActionResult> OnGetRenderAddPage()
        {
            return await AjaxTryCatch(async () =>
            {
                var view = await _renderViewToString.RenderToStringAsync("_Add", new PersonnelViewModel(), PageContext);
                return OperationResult<string>.Success(view);
            });
        }

        public async Task<IActionResult> OnPostAddPersonnel(PersonnelViewModel viewModel)
        {
            return await AjaxTryCatch(async () =>
            {
                var result = await _personnelService.AddAsync(viewModel);
                return result;
            }, true);
        }

        public async Task<IActionResult> OnGetRenderAddExcelPage()
        {
            return await AjaxTryCatch(async () =>
            {
                var view = await _renderViewToString.RenderToStringAsync("_AddExcel", new ExcelViewModel(), PageContext);
                return OperationResult<string>.Success(view);
            });
        }

        public async Task<IActionResult> OnPostAddExcel(ExcelViewModel viewModel)
        {
            return await AjaxTryCatch(async () =>
            {
                try
                {
                    var stream = new MemoryStream();
                    await viewModel.File.CopyToAsync(stream);
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
                                    ComputerCode = (worksheet.Cells[row, 1].Value ?? string.Empty).ToString().Trim(),
                                    Name = (worksheet.Cells[row, 2].Value ?? string.Empty).ToString().Trim(),
                                    Family = (worksheet.Cells[row, 3].Value ?? string.Empty).ToString().Trim(),
                                    FatherName = (worksheet.Cells[row, 4].Value ?? string.Empty).ToString().Trim(),
                                    NationalCode = (worksheet.Cells[row, 5].Value ?? string.Empty).ToString().Trim(),
                                    BirthCertificateNumber = (worksheet.Cells[row, 6].Value ?? string.Empty).ToString().Trim(),
                                    BirthDate = (worksheet.Cells[row, 7].Value ?? string.Empty).ToString().Trim(),
                                    PlaceOfBirth = (worksheet.Cells[row, 8].Value ?? string.Empty).ToString().Trim(),
                                    TypeOfEmploymentId = Convert.ToInt64((worksheet.Cells[row, 9].Value ?? string.Empty).ToString().Trim()),
                                    EducationDegreeId = Convert.ToInt64((worksheet.Cells[row, 10].Value ?? string.Empty).ToString().Trim()),
                                    StudyFieldId = Convert.ToInt64((worksheet.Cells[row, 11].Value ?? string.Empty).ToString().Trim()),
                                    LastPositionId = Convert.ToInt64((worksheet.Cells[row, 12].Value ?? string.Empty).ToString().Trim()),
                                    ServiceLocationId = Convert.ToInt64((worksheet.Cells[row, 13].Value ?? string.Empty).ToString().Trim()),
                                    MaritalStatusId = Convert.ToInt64((worksheet.Cells[row, 14].Value ?? string.Empty).ToString().Trim()),
                                });
                            }
                            var result = await _personnelService.AddExcelAsync(cardExcelsList);
                            return result;
                        }
                    }
                }
                catch (Exception ex)
                {
                    return new OperationResult();
                }
            }, true);
        }

        public async Task<IActionResult> OnGetExportExcel()
        {
            var personnels = await _personnelService.ReadAsync();

            // ایجاد فایل اکسل با استفاده از یک کتابخانه مانند EPPlus

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Personnels");

                // ایجاد هدرها
                worksheet.Cells[1, 1].Value = "شماره پرونده";
                worksheet.Cells[1, 2].Value = "وضعیت پرونده";
                worksheet.Cells[1, 3].Value = "نام";
                worksheet.Cells[1, 4].Value = "نام خانوادگی";
                worksheet.Cells[1, 5].Value = "نام پدر";
                worksheet.Cells[1, 6].Value = "کد ملی";
                worksheet.Cells[1, 7].Value = "شماره شناسنامه";
                worksheet.Cells[1, 8].Value = "تاریخ تولد";
                worksheet.Cells[1, 9].Value = "محل تولد";
                worksheet.Cells[1, 10].Value = "کد رایانه";
                worksheet.Cells[1, 11].Value = "نوع استخدام";
                worksheet.Cells[1, 12].Value = "مقطع تحصیلی";
                worksheet.Cells[1, 13].Value = "رشته تحصیلی";
                worksheet.Cells[1, 14].Value = "آخرین پست";
                worksheet.Cells[1, 15].Value = "محل خدمت فعلی";
                worksheet.Cells[1, 16].Value = "وضعیت تاهل";
                worksheet.Cells[1, 17].Value = "تاریخ ثبت";

                // پر کردن داده‌ها
                int row = 2;
                foreach (var personnel in personnels)
                {
                    worksheet.Cells[row, 1].Value = personnel.CaseNumber;
                    worksheet.Cells[row, 2].Value = personnel.CaseStatus;
                    worksheet.Cells[row, 3].Value = personnel.Name;
                    worksheet.Cells[row, 4].Value = personnel.Family;
                    worksheet.Cells[row, 5].Value = personnel.FatherName;
                    worksheet.Cells[row, 6].Value = personnel.NationalCode;
                    worksheet.Cells[row, 7].Value = personnel.BirthCertificateNumber;
                    worksheet.Cells[row, 8].Value = personnel.BirthDate;
                    worksheet.Cells[row, 9].Value = personnel.PlaceOfBirth;
                    worksheet.Cells[row, 10].Value = personnel.ComputerCode;
                    worksheet.Cells[row, 11].Value = personnel.TypeOfEmployment;
                    worksheet.Cells[row, 12].Value = personnel.EducationDegree;
                    worksheet.Cells[row, 13].Value = personnel.StudyField;
                    worksheet.Cells[row, 14].Value = personnel.LastPosition;
                    worksheet.Cells[row, 15].Value = personnel.ServiceLocation;
                    worksheet.Cells[row, 16].Value = personnel.MaritalStatus;
                    worksheet.Cells[row, 17].Value = personnel.RegDate;
                    row++;
                }

                // تنظیم فرمت فایل اکسل
                worksheet.Cells.AutoFitColumns();

                // دستکاری کدهای دیگر برای نیازهای خاص شما

                // تبدیل بسته فایل اکسل به آرایه بایت
                byte[] excelBytes = package.GetAsByteArray();

                // دانلود فایل اکسل
                return File(excelBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Personnel.xlsx");
            }
        }

        public async Task<IActionResult> OnGetRenderEditPage(long id)
        {
            var model = await _personnelService.FindAsync(id);
            return await AjaxTryCatch(async () =>
            {
                var view = await _renderViewToString.RenderToStringAsync("_Edit", new PersonnelViewModel()
                {
                    BirthCertificateNumber = model.Value.BirthCertificateNumber,
                    BirthDate = model.Value.BirthDate,
                    CaseNumber = model.Value.CaseNumber,
                    CaseStatusId = model.Value.CaseStatusId,
                    ComputerCode = model.Value.ComputerCode,
                    EducationDegreeId = model.Value.EducationDegreeId,
                    Family = model.Value.Family,
                    FatherName = model.Value.FatherName,
                    Id = model.Value.Id,
                    LastPositionId = model.Value.LastPositionId,
                    MaritalStatusId = model.Value.MaritalStatusId,
                    Name = model.Value.Name,
                    NationalCode = model.Value.NationalCode,
                    PlaceOfBirth = model.Value.PlaceOfBirth,
                    RegDate = model.Value.RegDate,
                    ServiceLocationId = model.Value.ServiceLocationId,
                    StudyFieldId = model.Value.StudyFieldId,
                    TypeOfEmploymentId = model.Value.TypeOfEmploymentId,
                    ServiceLocationName = model.Value.ServiceLocationName,
                    CaseStatusName = model.Value.CaseStatusName
                }, PageContext);
                return OperationResult<string>.Success(view);
            });
        }

        public async Task<IActionResult> OnPostEditPersonnel(PersonnelViewModel viewModel)
        {
            return await AjaxTryCatch(async () =>
            {
                var result = await _personnelService.UpdateAsync(viewModel);
                return result;
            }, true);
        }

        public async Task<IActionResult> OnPostToggleStatus(long id)
        {
            return await AjaxTryCatch(async () =>
                {
                    return await _personnelService.ToggleStatusAsync(id);
                });
        }
    }
}
