using RBAS.Libraries.Csharp.Server.Db.Types;

namespace RBAS.Libraries.Csharp.Server.Db.Repository.Interfaces;

public interface ICreateRepository<TEntity, TKey> : IRawRepository 
    where TEntity : class, IBaseDataType<TKey>
    where TKey : unmanaged, IComparable
{
    Task<TEntity> AddAsync(TEntity entity);
    Task<IEnumerable<TEntity>> AddAsync(ICollection<TEntity> entities);
}
