using DataModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModels.Models
{
    public class TypeOfPositionModel : IdentifierModel, IEntity
    {
        public string Title { get; set; }
        public virtual List<PersonnelsModel> Personnels { get; set; }
    }
}
