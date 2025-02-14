using System.Linq.Expressions;

namespace ECommerce.Repository
{
    public interface IRepository<T> where T : class
    {
        Task<T> Get(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> predicate = null);
        Task<T> Add(T entity);
        Task<T> Update(T entity);
        Task<bool> Delete(T entity);
    }
}
