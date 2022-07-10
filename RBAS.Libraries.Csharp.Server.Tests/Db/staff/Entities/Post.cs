using System.ComponentModel.DataAnnotations.Schema;
using RBAS.Libraries.Csharp.Server.Db.Types;

namespace RBAS.Libraries.Csharp.Server.Tests.Db.staff.Entities;

public class Post : BaseDataType<long>
{
    public string Title { get; set; }
    
    public string Content { get; set; }

    [ForeignKey(nameof(Blog))]
    public long BlogId { get; set; }
    public virtual Blog Blog { get; set; }
}