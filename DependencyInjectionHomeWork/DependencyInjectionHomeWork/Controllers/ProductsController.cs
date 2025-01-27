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

        public async Task<IActionResult> Update(int id)
        {
            var product = await productService.GetByIdAsync(id);

            if (product is null)
                return RedirectToAction(nameof(Index));
            var vm = new ProductUpdateViewModel()
            {
                Product = product
            };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Update(ProductUpdateViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(viewModel);

            await productService.Update(viewModel);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            await productService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
