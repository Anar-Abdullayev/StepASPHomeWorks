using ECommerce.Data;
using ECommerce.Entities;
using ECommerce.Repository.Abstract;
using ECommerce.Repository.EfRepositoryBase;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ECommerce.Repository.Concrete
{
    public class OrdersRepository : EfRepositoryBase<Order, ECommerceDbContext>, IOrdersRepository
    {
        private readonly ECommerceDbContext _context;
        public OrdersRepository(ECommerceDbContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<IEnumerable<Order>> GetAll(Expression<Func<Order, bool>> predicate = null)
        {
            return predicate is null
                ? await _context.Orders.Include(o => o.Product).Include(o => o.Customer).ToListAsync()
                : await _context.Orders.Where(predicate).Include(o => o.Product).Include(o => o.Customer).ToListAsync();
        }

        public override async Task<Order> Get(Expression<Func<Order, bool>> predicate)
        {
            var result = await _context.Orders.Include(o => o.Product).Include(o => o.Customer).FirstOrDefaultAsync(predicate);
            return result;
        }
    }
}
