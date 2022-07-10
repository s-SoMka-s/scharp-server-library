using Microsoft.EntityFrameworkCore;
using RBAS.Libraries.Csharp.Server.Db.Repository.Interfaces;
using RBAS.Libraries.Csharp.Server.Db.Types;
using System.Linq.Expressions;

namespace RBAS.Libraries.Csharp.Server.Db.Repository.Implementations
{
    public class CrudRepository<TEntity, TKey> : ICrudRepository<TEntity, TKey> 
        where TEntity : class, IBaseDataType<TKey>
        where TKey : unmanaged, IComparable
    {
        protected DbContext Context { get; }
        protected DbSet<TEntity> Set { get; }

        private readonly ICrudRepository<TEntity, TKey> self;

        public CrudRepository(DbContext context)
        {
            Context = context;
            Set = context.Set<TEntity>();

            self = this as ICrudRepository<TEntity, TKey>;
        }


        #region IRawRepository

        DbContext IRawRepository.Context => Context;

        #endregion IRawRepository

        #region ICreateRepository

        async Task<TEntity> ICreateRepository<TEntity, TKey>.AddAsync(TEntity entity)
        {
            var result = await self.AddAsync(new[] { entity });

            return result.First();
        }

        async Task<IEnumerable<TEntity>> ICreateRepository<TEntity, TKey>.AddAsync(ICollection<TEntity> entities)
        {
            await Context.AddRangeAsync(entities);
            await Context.SaveChangesAsync();

            return entities;
        }

        #endregion

        #region IReadRepository 

        async Task<TEntity> IReadRepository<TEntity, TKey>.FindAsync(TKey id)
        {
            var query = Set as IQueryable<TEntity>;
            var array = query.ToArray();
            var res = await query.SingleOrDefaultAsync(e => e.Id.Equals(id));

            return res;
        }

        async Task<TEntity> IReadRepository<TEntity, TKey>.FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var query = Set as IQueryable<TEntity>;
            var res = await query.SingleOrDefaultAsync(predicate);

            return res;
        }

        #endregion

        IQueryable<TEntity> ICrudRepository<TEntity, TKey>.Query()
        {
            var query = Set.AsNoTracking();

            return query;
        }

        #region IUpdateRepository

        async Task<TEntity> IUpdateRepository<TEntity, TKey>.UpdateAsync(TEntity updated)
        {
            var entity = Set.Update(updated).Entity;
            await Context.SaveChangesAsync();

            return entity;
        }

        async Task<TEntity> IUpdateRepository<TEntity, TKey>.UpdateAsync(TKey id, Action<TEntity> patch)
        {
            var entity = await self.FindAsync(id);

            patch(entity);
            Context.Entry(entity).State = EntityState.Modified;

            await Context.SaveChangesAsync();

            return entity;
        }

        #endregion

        #region IDeleteRepository 

        async Task<bool> IDeleteRepository<TEntity, TKey>.DeleteAsync(TEntity entity)
        {
            return await self.DeleteAsync(entity.Id);
        }

        async Task<bool> IDeleteRepository<TEntity, TKey>.DeleteAsync(TKey id)
        {
            return await self.DeleteAsync(new[] { id });
        }

        async Task<bool> IDeleteRepository<TEntity, TKey>.DeleteAsync(IEnumerable<TKey> ids)
        {
            var forRemove = ids.ToArray();

            return await self.DeleteAsync(e => forRemove.Contains(e.Id));
        }

        async Task<bool> IDeleteRepository<TEntity, TKey>.DeleteAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var forRemove = await Set.Where(predicate).ToArrayAsync();

            Set.RemoveRange(forRemove);
            await Context.SaveChangesAsync();

            return !await Set.AnyAsync(predicate);
        }

        #endregion
    }
}
