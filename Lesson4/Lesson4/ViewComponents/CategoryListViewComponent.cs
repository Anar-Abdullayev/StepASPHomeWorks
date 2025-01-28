using Lesson4.Data;
using Lesson4.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;

namespace Lesson4.ViewComponents
{
    public class CategoryListViewComponent : ViewComponent
    {
        private readonly ProductDbContext _context;
        public CategoryListViewComponent(ProductDbContext context)
        {
            _context = context;
        }

        public ViewViewComponentResult Invoke()
        {
            var categories = _context.Categories.Select(c=> new CategoryViewModel { Name = c.Name }).ToList();
            var vm = new CategoryListViewModel()
            {
                Categories = categories
            };
            return View(vm);
        }
    }
}
