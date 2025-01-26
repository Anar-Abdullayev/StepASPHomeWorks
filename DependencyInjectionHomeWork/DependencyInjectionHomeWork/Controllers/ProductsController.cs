using DependencyInjectionHomeWork.Models;
using DependencyInjectionHomeWork.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DependencyInjectionHomeWork.Controllers
{
    public class ProductsController(IProductService productService) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var products = await productService.GetAllAsync();
            return View(products);
        }

        public IActionResult Add()
        {
            var vm = new ProductAddViewModel();
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Add(ProductAddViewModel product)
        {
            if (!ModelState.IsValid)
                return View(product);

            await productService.Add(product);

            return RedirectToAction(nameof(Index));
        }
    }
}
