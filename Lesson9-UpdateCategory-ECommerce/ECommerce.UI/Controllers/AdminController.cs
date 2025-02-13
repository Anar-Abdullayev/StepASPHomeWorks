using ECommerce.Business.Abstract;
using ECommerce.Entities.Models;
using ECommerce.UI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace ECommerce.UI.Controllers
{

    //public class AdminController : Controller
    //{
    //    [Authorize(Roles = "Admin")]
    //    public IActionResult Index()
    //    {
    //        var username = User.Identity.Name;
    //        TempData["username"] = username;
    //        return View();
    //    }
    //    [Authorize(Roles = "Admin,Editor")]
    //    public IActionResult Index2()
    //    {
    //        return View();
    //    }
    //}

    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public AdminController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index(int page = 1, int category = 0)
        {
            int pageSize = 10;
            var items = await _productService.GetAllByCategoryAsync(category);

            var model = new ProductListViewModel
            {
                Products = items.Skip((page - 1) * pageSize).Take(pageSize).ToList(),
                PageCount = (int)Math.Ceiling(items.Count / (double)pageSize),
                PageSize = pageSize,
                CurrentPage = page,
                CurrentCategory = category
            };
            return View(model);
        }


        public async Task<IActionResult> RemoveProduct(int productId, int page = 1, int category = 0)
        {
            // await _productService.DeleteProduct(productId);
            TempData["message"] = "Product deleted successfully";
            return RedirectToAction("Index", new { page = page, category = category });
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCategory([FromBody] CategoryUpdateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _categoryService.UpdateAsync(model.CategoryId, model.CategoryNewName);
                return Json(JsonSerializer.Serialize(new { status = result }));
            }
            return Json(JsonSerializer.Serialize(new {status = ModelState[nameof(CategoryUpdateViewModel.CategoryNewName)]?.Errors[0].ErrorMessage }));
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory([FromBody] CategoryAddViewModel newCategory)
        {
            if (ModelState.IsValid)
            {
                var category = new Category
                {
                    CategoryName = newCategory.CategoryName,
                };
                string result = await _categoryService.AddCategoryAsync(category);
                return Json(JsonSerializer.Serialize(new { status = result }));
            }
            string? error = ModelState[nameof(CategoryAddViewModel.CategoryName)]?.Errors[0].ErrorMessage;
            return Json(JsonSerializer.Serialize(new { status = error }));
        }
    }
}
