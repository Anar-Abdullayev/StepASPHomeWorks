using System.ComponentModel.DataAnnotations;

namespace ECommerce.UI.Models
{
    public class CategoryAddViewModel
    {
        [Required]
        [MaxLength(15, ErrorMessage = "Can't be more than 15 characters")]
        public string CategoryName { get; set; }
    }
}
