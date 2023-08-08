using DataModels.Base;

namespace DataModels.Models
{
    public class CaseStatusModel : IdentifierModel, IEntity
    {
        public string Title { get; set; }
        public string TitleEN { get; set; }
        public virtual List<PersonnelsModel> Personnels { get; set; }
    }
}
