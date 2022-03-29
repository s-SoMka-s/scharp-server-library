using Microsoft.EntityFrameworkCore;

namespace RBAS.Libraries.Csharp.Server.Db.Repository.Interfaces
{
    public interface IRawRepository
    {
        DbContext Context { get; }
    }
}
