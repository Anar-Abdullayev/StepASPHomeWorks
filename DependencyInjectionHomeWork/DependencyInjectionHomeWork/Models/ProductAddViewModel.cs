using DependencyInjectionHomeWork.Entities;
using System.ComponentModel.DataAnnotations;

namespace DependencyInjectionHomeWork.Models
{
    public class ProductAddViewModel
    {
        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal Discount { get; set; }
        public decimal Price { get; set; }
        [Required]
        public IFormFile File { get; set; }
    }
    




    public static class ProductAddExtentionMethods
    {
        public static Product ToProduct(this ProductAddViewModel viewModel)
        {
            var product = new Product();

            product.Name = viewModel.Name;
            product.Description = viewModel.Description;
            product.Discount = viewModel.Discount;
            product.Price = viewModel.Price;

            return product;
        }
    }
}
