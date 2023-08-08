using DataModels.Base;
using System.Threading.Tasks;

namespace DataModels.Models
{
    public class UsersModel : IdentifierModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
