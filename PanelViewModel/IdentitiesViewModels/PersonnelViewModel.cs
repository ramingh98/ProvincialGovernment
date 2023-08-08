using Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace PanelViewModel.IdentitiesViewModels
{
    public class PersonnelViewModel
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "نام الزامیست")]
        [MaxLength(60, ErrorMessage = "نام نمیتواند بیشتر از 60 کاراکتر باشد")]
        [MinLength(3, ErrorMessage = "نام نمیتواند کمتر از 3 کاراکتر باشد")]
        public string Name { get; set; }

        [Required(ErrorMessage = "نام خانوادگی الزامیست")]
        [MaxLength(60, ErrorMessage = "نام خانوادگی نمیتواند بیشتر از 60 کاراکتر باشد")]
        [MinLength(3, ErrorMessage = "نام خانوادگی نمیتواند کمتر از 3 کاراکتر باشد")]
        public string Family { get; set; }

        [Required(ErrorMessage = "نام پدر الزامیست")]
        [MaxLength(60, ErrorMessage = "نام پدر نمیتواند بیشتر از 60 کاراکتر باشد")]
        [MinLength(3, ErrorMessage = "نام پدر نمیتواند کمتر از 3 کاراکتر باشد")]
        public string FatherName { get; set; }

        [Required(ErrorMessage = "کد ملی الزامیست")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "کد ملی معتبر وارد نمایید")]
        public string NationalCode { get; set; }

        [Required(ErrorMessage = "شماره شناسنامه الزامیست")]
        public string BirthCertificateNumber { get; set; }

        [Required(ErrorMessage = "تاریخ تولد الزامیست")]
        [RegularExpression(@"^\d{4}/\d{2}/\d{2}$", ErrorMessage ="تاریخ تولد معتبر وارد نمایید")]
        public string BirthDate { get; set; }

        [Required(ErrorMessage = "محل تولد الزامیست")]
        public string PlaceOfBirth { get; set; }

        [Required(ErrorMessage = "شماره پرونده الزامیست")]
        public long CaseNumber { get; set; }

        [Required(ErrorMessage = "کد رایانه الزامیست")]
        public string ComputerCode { get; set; }

        [Required(ErrorMessage = "نوع استخدام الزامیست")]
        public long TypeOfEmploymentId { get; set; }

        [Required(ErrorMessage = "مدرک تحصیلی الزامیست")]
        public long EducationDegreeId { get; set; }

        [Required(ErrorMessage = "رشته تحصیلی الزامیست")]
        public long StudyFieldId { get; set; }

        [Required(ErrorMessage = "عنوان آخرین پست الزامیست")]
        public long LastPositionId { get; set; }

        [Required(ErrorMessage = "محل خدمت فعلی الزامیست")]
        public long ServiceLocationId { get; set; }
        public string? ServiceLocationName { get; set; }

        [Required(ErrorMessage = "وضعیت تاهل الزامیست")]
        public long MaritalStatusId { get; set; }

        [Required(ErrorMessage = "وضعیت پرونده الزامیست")]
        public long CaseStatusId { get; set; }
        public string? CaseStatusName { get; set; }
        public DateTime? RegDate { get; set; }
    }
}
