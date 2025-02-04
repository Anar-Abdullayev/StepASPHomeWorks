using ECommerce.Business.Abstract;
using ECommerce.UI.Models;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.UI.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> Index(int page = 1, int category = 0, bool? az = null, bool? higherToLower = null)
        {
            int pageSize = 10;
            var items = await _productService.GetAllByCategoryAsync(category);
            if (az == true)
                items = items.OrderBy(p => p.ProductName).ToList();
            else if (az == false)
                items = items.OrderByDescending(p => p.ProductName).ToList();
            else if (higherToLower == true)
                items = items.OrderByDescending(p => p.UnitPrice).ToList();
            else if (higherToLower == false)
                items = items.OrderBy(p => p.UnitPrice).ToList();

            var model = new ProductListViewModel
            {
                Products = items.Skip((page - 1) * pageSize).Take(pageSize).ToList(),
                PageCount = (int)Math.Ceiling(items.Count / (double)pageSize),
                PageSize = pageSize,
                CurrentPage = page,
                CurrentCategory = category,
                AtoZ = az,
                HigherToLower = higherToLower
            };
            return View(model);
        }
    }
}
