using DependencyInjectionHomeWork.Entities;
using DependencyInjectionHomeWork.Models;
using DependencyInjectionHomeWork.Repository.Interfaces;
using DependencyInjectionHomeWork.Services.Interfaces;
using System.Reflection.Metadata.Ecma335;

namespace DependencyInjectionHomeWork.Services
{
    public class ProductService(IProductRepository productRepository, IWebHostEnvironment webHostEnvironment, IConfiguration configuration) : IProductService
    {
        public async Task Add(ProductAddViewModel viewModel)
        {
            string fileName = await UploadImageToRoot(viewModel.File);

            var product = viewModel.ToProduct();

            product.ImageLink = configuration["ImageSettings:BaseStoreUrl"] +fileName;
            await productRepository.AddProductAsync(product);
        }

        public async Task DeleteAsync(int id)
        {
            var product = await productRepository.GetByIdAsync(id);
            if (product is null)
                return;

            await productRepository.DeleteProductAsync(id);
            string imagePath = webHostEnvironment.WebRootPath + product.ImageLink;
            if (File.Exists(imagePath))
                File.Delete(imagePath);
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

        public async Task Update(ProductUpdateViewModel viewModel)
        {
            if (viewModel.File is not null)
            {
                var fileName = await UploadImageToRoot(viewModel.File);
                string imagePath = webHostEnvironment.WebRootPath+viewModel.Product.ImageLink;
                if (File.Exists(imagePath))
                    File.Delete(imagePath);
                viewModel.Product.ImageLink = configuration["ImageSettings:BaseStoreUrl"] + fileName;
            }

            await productRepository.UpdateProductAsync(viewModel.Product);
        }

        private async Task<string> UploadImageToRoot(IFormFile formFile)
        {
            var imageUploadFolderPath = Path.Combine(webHostEnvironment.WebRootPath, "images");

            if (!Directory.Exists(imageUploadFolderPath))
                Directory.CreateDirectory(imageUploadFolderPath);

            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(formFile.FileName);
            string filePath = Path.Combine(imageUploadFolderPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await formFile.CopyToAsync(stream);
            }

            return fileName;
        }
    }
}
