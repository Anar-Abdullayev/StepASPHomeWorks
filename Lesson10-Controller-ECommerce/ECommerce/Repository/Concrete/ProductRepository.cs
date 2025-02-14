using ECommerce.Data;
using ECommerce.Entities;
using ECommerce.Repository.Abstract;
using ECommerce.Repository.EfRepositoryBase;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ECommerce.Repository.Concrete
{
    public class ProductRepository : EfRepositoryBase<Product, ECommerceDbContext>, IProductRepository
    {
        private readonly ECommerceDbContext _context;
        public ProductRepository(ECommerceDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAll(Expression<Func<Product, bool>> predicate = null, string sortBy = null, SortOrder sortOrder = SortOrder.Ascending)
        {
            IQueryable<Product> query = _context.Products.Where(p => true);
            if (predicate != null)
                query = query.Where(predicate);
            
            if (sortBy == nameof(Product.Price) && sortOrder == SortOrder.Ascending)
                query = query.OrderBy(p => p.Price);
            else if (sortBy == nameof(Product.Price) && sortOrder == SortOrder.Descending)
                query = query.OrderByDescending(p => p.Price);


            if (sortBy == nameof(Product.Discount) && sortOrder == SortOrder.Ascending)
                query = query.OrderBy(p => p.Discount);
            else if (sortBy == nameof(Product.Discount) && sortOrder == SortOrder.Descending)
                query = query.OrderByDescending(p => p.Discount);

            return await query.ToListAsync();
        }
    }
}
