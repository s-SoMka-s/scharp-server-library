using Microsoft.EntityFrameworkCore;
using RBAS.Libraries.Csharp.Server.Db.Repository.Interfaces;
using RBAS.Libraries.Csharp.Server.Db.Types;

namespace RBAS.Libraries.Csharp.Server.Db.Repository.Implementations;

public class CrudFactory<TContext> : ICrudFactory where TContext : DbContext
{
    private readonly IServiceProvider provider;
    private readonly Dictionary<Type, Type> typesСompliance;

    public CrudFactory(IServiceProvider provider)
    {
        this.provider = provider;
        typesСompliance = new Dictionary<Type, Type>();
    }

    public ICrudRepository<TEntity, TKey> Get<TEntity, TKey>()
        where TEntity : class, IBaseDataType<TKey>
        where TKey : unmanaged, IComparable
    {
        var type = typeof(TEntity);
        
        if (typesСompliance.ContainsKey(type))
        {
            return provider.GetService(typesСompliance[type]) as ICrudRepository<TEntity, TKey>;
        }

        return new CrudRepository<TEntity, TKey>(DefaultContext);
    }
    
    /*public ICrudRepository<TEntity> Get<TEntity>() where TEntity : class, IBaseDataType
    {
        return Get<TEntity>(DefaultContext);
    }

    public ICrudRepository<TEntity> Get<TEntity>(DbContext context) where TEntity : class, IBaseDataType
    {
        var type = typeof(TEntity);

        if (typesСompliance.ContainsKey(type))
        {
            return provider.GetService(typesСompliance[type]) as ICrudRepository<TEntity>;
        }

        return new CrudRepository<TEntity>(context);
    }*/

    public DbContext Context => DefaultContext;

    private TContext DefaultContext => provider.GetService(typeof(TContext)) as TContext ?? throw new InvalidOperationException();
}
