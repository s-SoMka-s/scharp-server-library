using RBAS.Libraries.Csharp.Server.Db.Types;
using System.Linq.Expressions;

namespace RBAS.Libraries.Csharp.Server.Db.Repository.Interfaces;

public interface IDeleteRepository<TEntity> where TEntity : class, IBaseDataType
{
    Task<bool> DeleteAsync(TEntity entity);
    Task<bool> DeleteAsync(Guid id);
    Task<bool> DeleteAsync(IEnumerable<Guid> ids);
    Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> predicate);
}
