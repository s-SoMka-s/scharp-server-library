using System.ComponentModel.DataAnnotations;

namespace RBAS.Libraries.Csharp.Server.Db.Types
{
    public class BaseDataType : IBaseDataType
    {
        protected BaseDataType()
        {
            Id = new Guid();
            CreatedAt = DateTimeOffset.UtcNow;
        }

        [Key]
        public Guid Id { get; set; }

        public DateTimeOffset CreatedAt { get; set; }
    }
}
