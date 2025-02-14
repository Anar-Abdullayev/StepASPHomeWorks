using ECommerce.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ECommerce.Repository.EfRepositoryBase
{
    public class EfRepositoryBase<TEntity, TContext> : IRepository<TEntity> where TEntity : class where TContext : DbContext
    {
        private readonly TContext _context;

        public EfRepositoryBase(TContext context)
        {
            _context = context;
        }

        public virtual async Task<TEntity> Add(TEntity entity)
        {
            var addedEntity = _context.Entry(entity);
            addedEntity.State = EntityState.Added;
            await _context.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<bool> Delete(TEntity entity)
        {
            var deletedEntity = _context.Entry(entity); 
            deletedEntity.State = EntityState.Deleted;
            return await _context.SaveChangesAsync() > 0;
        }

        public virtual async Task<TEntity> Get(Expression<Func<TEntity, bool>> predicate)
        {
            return await _context.Set<TEntity>().FirstOrDefaultAsync(predicate);
        }

        public virtual async Task<IEnumerable<TEntity>> GetAll(Expression<Func<TEntity, bool>> predicate = null)
        {
            return predicate == null ?
               await _context.Set<TEntity>().ToListAsync() :
               await _context.Set<TEntity>().Where(predicate).ToListAsync();
        }

        public virtual async Task<TEntity> Update(TEntity entity)
        {
            var updatedEntity = _context.Entry(entity);
            updatedEntity.State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
