using RBAS.Libraries.Csharp.Server.Db.Repository.Interfaces;
using RBAS.Libraries.Csharp.Server.Db.Types;

namespace RBAS.Libraries.Csharp.Server.Db.Repository;

public interface IUpdateRepository<TEntity> : IRawRepository where TEntity : class, IBaseDataType
{
    Task<TEntity> UpdateAsync(TEntity updated);

    Task<TEntity> UpdateAsync(Guid id, Action<TEntity> patch);
}
