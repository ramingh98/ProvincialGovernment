using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace PanelViewModel.IdentitiesViewModels
{
    public class ExcelViewModel
    {
        [Required(ErrorMessage = "فایل را انتخاب نمایید")]
        [FileExtensions(Extensions = "xlsx", ErrorMessage = "فایل معتبر وارد نمایید")]
        public IFormFile File { get; set; }

        public string Name { get; set; }
        public string Family { get; set; }
        public string FatherName { get; set; }
        public string NationalCode { get; set; }
        public string BirthCertificateNumber { get; set; }
        public string BirthDate { get; set; }
        public string PlaceOfBirth { get; set; }
        public long CaseNumber { get; set; }
        public string ComputerCode { get; set; }
        public string TypeOfEmployment { get; set; }
        public string EducationDegree { get; set; }
        public string StudyField { get; set; }
        public string LastPosition { get; set; }
        public string ServiceLocation { get; set; }
        public string MaritalStatus { get; set; }
        public string CaseStatus { get; set; }
        public string RegDate { get; set; }
    }
}
