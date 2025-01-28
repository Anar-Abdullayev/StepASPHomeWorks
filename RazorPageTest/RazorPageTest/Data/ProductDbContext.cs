using Microsoft.EntityFrameworkCore;
using RazorPageTest.Entities;
using System.Collections.Generic;

namespace RazorPageTest.Data
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
    }
}
