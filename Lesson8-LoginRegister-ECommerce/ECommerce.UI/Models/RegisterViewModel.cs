﻿using System.ComponentModel.DataAnnotations;

namespace ECommerce.UI.Models
{
    public class RegisterViewModel
    {
        [Required]
        public string? Username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
        public bool IsAdmin { get; set; }
    }
}
