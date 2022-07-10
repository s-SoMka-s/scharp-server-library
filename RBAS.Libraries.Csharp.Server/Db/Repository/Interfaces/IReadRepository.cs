using RBAS.Libraries.Csharp.Server.Db.Types;
using System.Linq.Expressions;

namespace RBAS.Libraries.Csharp.Server.Db.Repository.Interfaces;

public interface IReadRepository<TEntity, TKey> : IRawRepository
    where TEntity : class, IBaseDataType<TKey>
    where TKey : unmanaged, IComparable
{
    Task<TEntity> FindAsync(TKey id);
    Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate);
}
