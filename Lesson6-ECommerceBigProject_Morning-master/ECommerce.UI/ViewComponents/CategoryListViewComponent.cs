using ECommerce.Business.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;

namespace ECommerce.UI.ViewComponents
{
    public class CategoryListViewComponent(ICategoryService categoryService) : ViewComponent
    {
        public async Task<ViewViewComponentResult> InvokeAsync()
        {
            var categories = await categoryService.GetAllAsync();
            var param = HttpContext.Request.Query["category"];
            var category = int.TryParse(param, out var categoryId);
            var model = new CategoryListViewModel
            {
                Categories = categories,
                CurrentCategory = category ? categoryId : 0
            };
            return View(model);
        }
    }
}
