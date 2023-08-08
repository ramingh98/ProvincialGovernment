
namespace DataModels.Base
{
    public interface IEntity
    {
    }

    public class IdentifierModel
    {
        public long Id { get; set; }
        public DateTime RegDate { get; set; }

        public IdentifierModel()
        {
            RegDate = DateTime.Now;
        }
    }
}
