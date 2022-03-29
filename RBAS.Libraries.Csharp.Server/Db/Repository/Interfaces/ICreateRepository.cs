using RBAS.Libraries.Csharp.Server.Db.Types;

namespace RBAS.Libraries.Csharp.Server.Db.Repository.Interfaces;

public interface ICreateRepository<TEntity> : IRawRepository where TEntity : class, IBaseDataType
{
    Task<TEntity> AddAsync(TEntity entity);
    Task<IEnumerable<TEntity>> AddAsync(IEnumerable<TEntity> entities);
}
