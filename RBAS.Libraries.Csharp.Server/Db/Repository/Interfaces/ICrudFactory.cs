using Microsoft.EntityFrameworkCore;
using RBAS.Libraries.Csharp.Server.Db.Types;

namespace RBAS.Libraries.Csharp.Server.Db.Repository.Interfaces;

public interface ICrudFactory
{
    DbContext Context { get; }
    
    ICrudRepository<TEntity, TKey> Get<TEntity,TKey>() where TEntity : class, IBaseDataType<TKey> where TKey : unmanaged, IComparable;
    //ICrudRepository<TEntity> Get<TEntity>(DbContext context) where TEntity : class, IBaseDataType;
}
