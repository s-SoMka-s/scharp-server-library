using RBAS.Libraries.Csharp.Server.Db.Repository;
using RBAS.Libraries.Csharp.Server.Db.Types;

namespace RBAS.Libraries.Csharp.Server.Db.Repository.Interfaces;

public interface ICrudRepository<T> : ICreateRepository<T>, IReadRepository<T>, IUpdateRepository<T>, IDeleteRepository<T> where T : class, IBaseDataType
{
    IQueryable<T> Query();
}
