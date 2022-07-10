using RBAS.Libraries.Csharp.Server.Db.Types;
using System.Linq.Expressions;

namespace RBAS.Libraries.Csharp.Server.Db.Repository.Interfaces;

public interface IDeleteRepository<TEntity, TKey> 
    where TEntity : class, IBaseDataType<TKey>
    where TKey : unmanaged, IComparable
{
    Task<bool> DeleteAsync(TEntity entity);
    Task<bool> DeleteAsync(TKey id);
    Task<bool> DeleteAsync(IEnumerable<TKey> ids);
    Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> predicate);
}
