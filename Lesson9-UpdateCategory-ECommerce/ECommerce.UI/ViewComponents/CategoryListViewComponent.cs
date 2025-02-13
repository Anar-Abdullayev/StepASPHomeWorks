using ECommerce.Business.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;

namespace ECommerce.UI.ViewComponents
{
    public class CategoryListViewComponent:ViewComponent
    {
        private readonly ICategoryService _categoryService;

        public CategoryListViewComponent(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<ViewViewComponentResult> InvokeAsync(bool isAdmin=false)
        {
            var categories=await _categoryService.GetAllAsync();
            var param = HttpContext.Request.Query["category"];
            var category=int.TryParse(param, out var categoryId);
            var model = new CategoryListViewModel
            {
                isAdmin=isAdmin,
                Categories = categories,
                CurrentCategory = category ? categoryId : 0,
            };
            return View(model); 
        }
    }
}
