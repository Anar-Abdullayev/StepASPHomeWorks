using DependencyInjectionHomeWork.Entities;
using DependencyInjectionHomeWork.Models;

namespace DependencyInjectionHomeWork.Services.Interfaces
{
    public interface IProductService
    {
        Task<List<Product>?> GetAllAsync();
        Task<Product?> GetByIdAsync(int id);
        Task Add(ProductAddViewModel viewModel); 
        Task Update(ProductUpdateViewModel viewModel);
        Task DeleteAsync(int id);
    }
}
