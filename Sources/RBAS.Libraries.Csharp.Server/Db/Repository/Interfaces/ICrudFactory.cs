using Microsoft.EntityFrameworkCore;
using RBAS.Libraries.Csharp.Server.Db.Types;

namespace RBAS.Libraries.Csharp.Server.Db.Repository.Interfaces;

public interface ICrudFactory
{
    DbContext Context { get; }

    ICrudRepository<TEntity> Get<TEntity>() where TEntity : class, IBaseDataType;
    ICrudRepository<TEntity> Get<TEntity>(DbContext context) where TEntity : class, IBaseDataType;
}
