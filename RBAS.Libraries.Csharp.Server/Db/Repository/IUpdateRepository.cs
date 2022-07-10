using RBAS.Libraries.Csharp.Server.Db.Repository.Interfaces;
using RBAS.Libraries.Csharp.Server.Db.Types;

namespace RBAS.Libraries.Csharp.Server.Db.Repository;

public interface IUpdateRepository<TEntity, TKey> : IRawRepository
    where TEntity : class, IBaseDataType<TKey> 
    where TKey : unmanaged, IComparable
{
    Task<TEntity> UpdateAsync(TEntity updated);

    Task<TEntity> UpdateAsync(TKey id, Action<TEntity> patch);
}