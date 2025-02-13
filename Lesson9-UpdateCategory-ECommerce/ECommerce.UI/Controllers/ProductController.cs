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

        public async Task<IActionResult> Index(int page=1,int category=0)
        {
            int pageSize = 10;
            var items=await _productService.GetAllByCategoryAsync(category);
            
            var model=new ProductListViewModel { 
                Products = items.Skip((page-1)*pageSize).Take(pageSize).ToList(),
                PageCount=(int)Math.Ceiling(items.Count/(double)pageSize),
                PageSize=pageSize,
                CurrentPage=page,
                CurrentCategory=category
            };
            return View(model);
        }
    }
}
