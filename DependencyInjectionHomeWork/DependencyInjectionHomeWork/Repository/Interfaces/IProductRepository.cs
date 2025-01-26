using DependencyInjectionHomeWork.Entities;

namespace DependencyInjectionHomeWork.Repository.Interfaces
{
    public interface IProductRepository
    {
        Task AddProductAsync(Product product);
        Task<List<Product>?> GetAllAsync();
        Task<Product?> GetByIdAsync(int id);
        Task UpdateProductAsync(Product product);
        Task DeleteProductAsync(int id);
    }
}
