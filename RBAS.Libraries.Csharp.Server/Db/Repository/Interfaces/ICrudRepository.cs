using RBAS.Libraries.Csharp.Server.Db.Types;

namespace RBAS.Libraries.Csharp.Server.Db.Repository.Interfaces;

public interface ICrudRepository<TEntity, TKey> : ICreateRepository<TEntity, TKey>, IReadRepository<TEntity, TKey>, IUpdateRepository<TEntity, TKey>, IDeleteRepository<TEntity, TKey> 
    where TEntity : class, IBaseDataType<TKey>
    where TKey : unmanaged, IComparable
{
    IQueryable<TEntity> Query();
}
