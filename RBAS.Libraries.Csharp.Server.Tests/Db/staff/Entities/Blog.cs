using RBAS.Libraries.Csharp.Server.Db.Types;

namespace RBAS.Libraries.Csharp.Server.Tests.Db.staff.Entities;

public class Blog : BaseDataType<long>
{
    public string Name { get; set; }
    
    public string Url { get; set; }
}