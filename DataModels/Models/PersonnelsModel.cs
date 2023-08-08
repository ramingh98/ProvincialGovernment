using DataModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModels.Models
{
    public class PersonnelsModel: IdentifierModel, IEntity
    {
        public string Name { get; set; }
        public string Family { get; set; }
        public string FatherName { get; set; }
        public string NationalCode { get; set; }
        public string BirthCertificateNumber { get; set; }
        public string BirthDate { get; set; }
        public string PlaceOfBirth { get; set; }
        public long CaseNumber { get; set; }
        public string ComputerCode { get; set; }
        public long TypeOfEmploymentId { get; set; }
        public virtual TypeOfEmploymentModel TypeOfEmployment { get; set; }
        public long EducationDegreeId { get; set; }
        public virtual EducationDegreeModel EducationDegree { get; set; }
        public long StudyFieldId { get; set; }
        public virtual StudyFieldModel StudyField { get; set; }
        public long LastPositionId { get; set; }
        public virtual TypeOfPositionModel TypeOfPosition { get; set; }
        public long ServiceLocationId { get; set; }
        public virtual ServiceLocationModel ServiceLocation { get; set; }
        public long MaritalStatusId { get; set; }
        public virtual MaritalStatusModel MaritalStatus { get; set; }
        public long CaseStatusId { get; set; }
        public virtual CaseStatusModel CaseStatus { get; set; }
    }
}
