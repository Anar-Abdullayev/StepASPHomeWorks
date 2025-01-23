using Asp.NetCoreLesson2.Entities;
using Asp.NetCoreLesson2.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Asp.NetCoreLesson2.Controllers
{
    public class ProductsController : Controller
    {
        static List<Product> products = new List<Product>
        {
            new Product(){ Id = 1, Description = "Some description", Name = "Coca Cola", Price = 1.5m, Discount = 0 },
            new Product(){ Id = 2, Description = "Some description", Name = "Pepsi", Price = 1.4m, Discount = 0 },
            new Product(){ Id = 3, Description = "Some description", Name = "Sprite", Price = 1.3m, Discount = 0.1f },
            new Product(){ Id = 4, Description = "Some description", Name = "Fanta", Price = 1.2m, Discount = 0.05f },
            new Product(){ Id = 5, Description = "Some description", Name = "Mountain Dew", Price = 1.6m, Discount = 0 },
            new Product(){ Id = 6, Description = "Some description", Name = "Dr Pepper", Price = 1.5m, Discount = 0.1f },
            new Product(){ Id = 7, Description = "Some description", Name = "7 Up", Price = 1.3m, Discount = 0 },
            new Product(){ Id = 8, Description = "Some description", Name = "Mirinda", Price = 1.2m, Discount = 0.05f },
            new Product(){ Id = 9, Description = "Some description", Name = "Lipton Ice Tea", Price = 1.7m, Discount = 0 },
            new Product(){ Id = 10, Description = "Some description", Name = "Red Bull", Price = 2.0m, Discount = 0.2f },
        };

        public IActionResult Index()
        {
            return View(products);
        }
        public IActionResult Add()
        {
            var vm = new ProductAddViewModel
            {
                Product = new Product()
            };
            return View(vm);
        }

        [HttpPost]
        public IActionResult Add(ProductAddViewModel productAddViewModel)
        {
            if (ModelState.IsValid)
            {
                var lastProduct = products.LastOrDefault();
                int lastId = 1;
                if (lastProduct != null) 
                    lastId = lastProduct.Id + 1;
                productAddViewModel.Product.Id = lastId;
                products.Add(productAddViewModel.Product);
                return RedirectToAction(nameof(Index));
            }
            return View(productAddViewModel);
        }

        public IActionResult Delete(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product is not null)
            {
                products.Remove(product);
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Update(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);
            var vm = new ProductUpdateViewModel { Product = product };
            if (product is not null)
                return View(vm);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Update(ProductUpdateViewModel productUpdateViewModel)
        {
            if (ModelState.IsValid)
            {
                var originalProduct = products.FirstOrDefault(p => p.Id == productUpdateViewModel.Product.Id);
                if (originalProduct is not null)
                {
                    originalProduct.Name = productUpdateViewModel.Product.Name;
                    originalProduct.Description = productUpdateViewModel.Product.Description;
                    originalProduct.Price = productUpdateViewModel.Product.Price;
                    originalProduct.Discount = productUpdateViewModel.Product.Discount;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(productUpdateViewModel);
        }
    }
}
