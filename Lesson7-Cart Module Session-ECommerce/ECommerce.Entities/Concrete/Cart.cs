﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Entities.Concrete
{
    public class Cart
    {
        public List<CartLine>? CartLines { get; set; }
        public Cart() {
            CartLines = new List<CartLine>();
        }

        public decimal Total
        {
            get
            {
                return (decimal)CartLines.Sum(c=>c.Product.UnitPrice * c.Quantity);
            }
        }
    }
}
