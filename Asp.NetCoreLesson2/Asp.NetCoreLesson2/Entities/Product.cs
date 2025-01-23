using System.ComponentModel.DataAnnotations;

namespace Asp.NetCoreLesson2.Entities
{
    public class Product
    {
        public int Id { get; set; }
        [MinLength(5, ErrorMessage = "Products name can't be less than 5 characters")]
        [Required(ErrorMessage = "Product name can't be empty!")]
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public float Discount { get; set; }
    }
}
