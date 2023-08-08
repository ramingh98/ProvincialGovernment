using DataModels.Base;

namespace DataModels.Models
{
    public class EducationDegreeModel : IdentifierModel, IEntity
    {
        public string Title { get; set; }
        public string TitleEn { get; set; }
        public virtual List<PersonnelsModel> Personnels { get; set; }
    }
}
