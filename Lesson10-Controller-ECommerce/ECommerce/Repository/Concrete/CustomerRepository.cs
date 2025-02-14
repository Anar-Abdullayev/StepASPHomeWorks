using ECommerce.Data;
using ECommerce.Entities;
using ECommerce.Repository.Abstract;
using ECommerce.Repository.EfRepositoryBase;
using System.Linq.Expressions;

namespace ECommerce.Repository.Concrete
{
    public class CustomerRepository : EfRepositoryBase<Customer, ECommerceDbContext>, ICustomerRepository
    {
        public CustomerRepository(ECommerceDbContext context) : base(context)
        {

        }
    }
}
