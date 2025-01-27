using DependencyInjectionHomeWork.Entities;
using System.ComponentModel.DataAnnotations;

namespace DependencyInjectionHomeWork.Models
{
    public class ProductUpdateViewModel
    {
        public Product Product { get; set; }
        public IFormFile? File { get; set; }
    }
}
