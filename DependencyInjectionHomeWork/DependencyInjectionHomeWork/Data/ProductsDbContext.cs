using DependencyInjectionHomeWork.Entities;
using Microsoft.EntityFrameworkCore;

namespace DependencyInjectionHomeWork.Data
{
    public class ProductsDbContext : DbContext
    {
        public ProductsDbContext(DbContextOptions<ProductsDbContext> options) : base(options)
        {
            
        }

        public DbSet<Product> Products { get; set; }
    }
}
