using Microsoft.EntityFrameworkCore;
using RBAS.Libraries.Csharp.Server.Db.Repository;
using RBAS.Libraries.Csharp.Server.Db.Repository.Interfaces;
using RBAS.Libraries.Csharp.Server.Db.Types;
using System.Linq.Expressions;

namespace RBAS.Libraries.Csharp.Server.Db.Repository.Implementations
{
    public class CrudRepository<TEntity> : ICrudRepository<TEntity> where TEntity : class, IBaseDataType
    {
        protected DbContext Context { get; }
        protected DbSet<TEntity> Set { get; }

        private readonly ICrudRepository<TEntity> self;

        public CrudRepository(DbContext context)
        {
            Context = context;
            Set = context.Set<TEntity>();

            self = this as ICrudRepository<TEntity>;
        }


        #region IRawRepository

        DbContext IRawRepository.Context => Context;

        #endregion IRawRepository

        #region ICreateRepository

        async Task<TEntity> ICreateRepository<TEntity>.AddAsync(TEntity entity)
        {
            var result = await self.AddAsync(new[] { entity });

            return result.First();
        }

        async Task<IEnumerable<TEntity>> ICreateRepository<TEntity>.AddAsync(IEnumerable<TEntity> entities)
        {
            await Context.AddRangeAsync(entities);
            await Context.SaveChangesAsync();

            return entities;
        }

        #endregion

        #region IReadRepository 

        async Task<TEntity> IReadRepository<TEntity>.FindAsync(Guid id)
        {
            var query = Set as IQueryable<TEntity>;
            var res = await query.SingleOrDefaultAsync(e => e.Id == id);

            return res;
        }

        async Task<TEntity> IReadRepository<TEntity>.FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var query = Set as IQueryable<TEntity>;
            var res = await query.SingleOrDefaultAsync(predicate);

            return res;
        }

        #endregion

        IQueryable<TEntity> ICrudRepository<TEntity>.Query()
        {
            var query = Set.AsNoTracking();

            return query;
        }

        #region IUpdateRepository

        async Task<TEntity> IUpdateRepository<TEntity>.UpdateAsync(TEntity updated)
        {
            var entity = Set.Update(updated).Entity;
            await Context.SaveChangesAsync();

            return entity;
        }

        async Task<TEntity> IUpdateRepository<TEntity>.UpdateAsync(Guid id, Action<TEntity> patch)
        {
            var entity = await self.FindAsync(id);

            patch(entity);
            Context.Entry(entity).State = EntityState.Modified;

            await Context.SaveChangesAsync();

            return entity;
        }

        #endregion

        #region IDeleteRepository 

        async Task<bool> IDeleteRepository<TEntity>.DeleteAsync(TEntity entity)
        {
            return await self.DeleteAsync(entity.Id);
        }

        async Task<bool> IDeleteRepository<TEntity>.DeleteAsync(Guid id)
        {
            return await self.DeleteAsync(new[] { id });
        }

        async Task<bool> IDeleteRepository<TEntity>.DeleteAsync(IEnumerable<Guid> ids)
        {
            var forRemove = ids.ToArray();

            return await self.DeleteAsync(e => forRemove.Contains(e.Id));
        }

        async Task<bool> IDeleteRepository<TEntity>.DeleteAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var forRemove = await Set.Where(predicate).ToArrayAsync();

            Set.RemoveRange(forRemove);
            await Context.SaveChangesAsync();

            return !await Set.AnyAsync(predicate);
        }

        #endregion
    }
}
