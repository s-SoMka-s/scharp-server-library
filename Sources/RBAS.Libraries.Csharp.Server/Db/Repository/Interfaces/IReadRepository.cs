using RBAS.Libraries.Csharp.Server.Db.Types;
using System.Linq.Expressions;

namespace RBAS.Libraries.Csharp.Server.Db.Repository.Interfaces;

public interface IReadRepository<TEntity> : IRawRepository where TEntity : class, IBaseDataType
{
    Task<TEntity> FindAsync(Guid id);
    Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate);
}
