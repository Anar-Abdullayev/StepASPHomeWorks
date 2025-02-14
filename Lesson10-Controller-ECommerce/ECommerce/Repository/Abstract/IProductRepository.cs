using ECommerce.Entities;
using Microsoft.Data.SqlClient;
using System.Linq.Expressions;

namespace ECommerce.Repository.Abstract
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IEnumerable<Product>> GetAll(Expression<Func<Product,bool>> predicate = null, string sortBy = null, SortOrder sortOrder = SortOrder.Ascending);
    }
}
