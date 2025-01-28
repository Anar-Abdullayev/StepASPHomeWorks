using System.Diagnostics;
using Lesson4.Data;
using Lesson4.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lesson4.Controllers
{
    public class HomeController(ProductDbContext context) : Controller
    {
        public IActionResult Index()
        {
            var products = context.Products.Select(p=> new ProductViewModel
            {
                Name = p.Name,
                Price = p.Price,
            }).ToList();

            var categories = context.Categories.Select(c => new CategoryViewModel { Name = c.Name }).ToList();

            var vm = new ProductCategoryViewModel
            {
                Products = products,
                Categories = categories
            };
            return View(vm);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
