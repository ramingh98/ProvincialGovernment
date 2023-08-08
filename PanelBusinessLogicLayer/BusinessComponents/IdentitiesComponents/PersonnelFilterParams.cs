using Common.Enums;
using Common.Filter;

namespace PanelBusinessLogicLayer.BusinessComponents.IdentitiesComponents
{
    public class PersonnelFilterParams : BaseFilterParam
    {
        public string? FullName { get; set; }
        public string? NationalCode { get; set; }
        public long? EmploymentTypeId { get; set; }
        public long? CaseStatus { get; set; }
        public string? ServiceLocation { get; set; }
        public long? CaseNumber { get; set; }
        public long? EducationDegreeId { get; set; }
    }
}
