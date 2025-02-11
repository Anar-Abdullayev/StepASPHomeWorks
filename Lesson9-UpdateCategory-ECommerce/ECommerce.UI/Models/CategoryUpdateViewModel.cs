using System.ComponentModel.DataAnnotations;

namespace ECommerce.UI.Models
{
    public class CategoryUpdateViewModel
    {
        public int CategoryId { get; set; }
        [Required]
        [MaxLength(15, ErrorMessage = "Can't be longer than 15")]
        public string CategoryNewName { get; set; }
    }
}
