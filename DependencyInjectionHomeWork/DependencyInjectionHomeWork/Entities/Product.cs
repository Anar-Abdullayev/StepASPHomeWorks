﻿using System.ComponentModel.DataAnnotations;

namespace DependencyInjectionHomeWork.Entities
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal Discount { get; set; }
        public decimal Price { get; set; }
        public string ImageLink { get; set; }

    }
}
