using DependencyInjectionHomeWork.Data;
using DependencyInjectionHomeWork.Entities;
using DependencyInjectionHomeWork.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DependencyInjectionHomeWork.Repository
{
    public class ProductRepository(ProductsDbContext context) : IProductRepository
    {
        public async Task AddProductAsync(Product product)
        {
            await context.Products.AddAsync(product);
            await context.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(int id)
        {
            var product = await context.Products.SingleOrDefaultAsync(product => product.Id == id);
            if (product is not null)
            {
                context.Products.Remove(product);
                await context.SaveChangesAsync();
            }
        }

        public async Task<List<Product>?> GetAllAsync()
        {
            return await context.Products.ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            var product = await context.Products.SingleOrDefaultAsync(product => product.Id == id);
            return product;
        }

        public async Task UpdateProductAsync(Product product)
        {
            var currentProduct = await context.Products.SingleOrDefaultAsync(prod => prod.Id == product.Id);
            if (currentProduct is null)
                return;
            currentProduct.Name = product.Name;
            currentProduct.Description = product.Description;
            currentProduct.Price = product.Price;
            currentProduct.Discount = product.Discount;
            currentProduct.ImageLink = product.ImageLink;

            await context.SaveChangesAsync();
        }
    }
}
