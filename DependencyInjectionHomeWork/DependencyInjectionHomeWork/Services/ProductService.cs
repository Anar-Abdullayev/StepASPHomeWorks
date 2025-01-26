using DependencyInjectionHomeWork.Entities;
using DependencyInjectionHomeWork.Models;
using DependencyInjectionHomeWork.Repository.Interfaces;
using DependencyInjectionHomeWork.Services.Interfaces;
using System.Reflection.Metadata.Ecma335;

namespace DependencyInjectionHomeWork.Services
{
    public class ProductService(IProductRepository productRepository, IWebHostEnvironment webHostEnvironment) : IProductService
    {
        public async Task Add(ProductAddViewModel viewModel)
        {
            var imageUploadFolderPath = Path.Combine(webHostEnvironment.WebRootPath, "images");
            
            if (!Directory.Exists(imageUploadFolderPath))
                Directory.CreateDirectory(imageUploadFolderPath);

            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(viewModel.File.FileName);
            string filePath = Path.Combine(imageUploadFolderPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await viewModel.File.CopyToAsync(stream);
            }

            var product = viewModel.ToProduct();

            product.ImageLink = @"\images\"+fileName;
            await productRepository.AddProductAsync(product);
        }

        public async Task<List<Product>?> GetAllAsync()
        {
            var products = await productRepository.GetAllAsync();
            return products;
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            var product = await productRepository.GetByIdAsync(id);
            return product;
        }
    }
}
