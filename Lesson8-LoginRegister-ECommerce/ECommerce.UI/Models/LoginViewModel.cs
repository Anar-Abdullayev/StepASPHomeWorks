﻿using System.ComponentModel.DataAnnotations;

namespace ECommerce.UI.Models
{
    public class LoginViewModel
    {
        [Required]
        public string? Username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        public bool RememberMe { get; set; }
        public string? ReturnUrl { get; set; }
        public bool IsAdmin { get; set; }

    }
}
