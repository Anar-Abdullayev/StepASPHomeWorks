using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPageTest.Data;
using RazorPageTest.Entities;

namespace RazorPageTest.Pages.Product
{
    public class IndexModel : PageModel
    {
        private readonly ProductDbContext _context;

        public IndexModel(ProductDbContext context)
        {
            _context = context;
        }

        public List<Entities.Product> Products { get; set; }
        public string Info { get; set; }
        public bool IsEditMode => Product?.Id > 0;

        [BindProperty]
        public Entities.Product Product { get; set; }

        public void OnGet(string info = "")
        {
            Products = _context.Products.ToList();
            Info = info;
        }

        public IActionResult OnPostAdd()
        {
            var newProduct = new Entities.Product()
            {
                Name = Product.Name,
                Price = Product.Price,
            };
            _context.Products.Add(newProduct);
            _context.SaveChanges();
            Info = $"{newProduct.Name} added successfully!";

            return RedirectToPage("Index", new { info = Info });
        }

        public IActionResult OnPostSave()
        {
            var product = _context.Products.SingleOrDefault(p => p.Id == Product.Id);
            if (product != null)
            {
                product.Name = Product.Name;
                product.Price = Product.Price;
                _context.SaveChanges();
                return RedirectToPage("Index", new { info = "Product has been updated successfully" });
            }
            return RedirectToPage("Index", new { info = "Product not found!" });
        }

        public IActionResult OnPostEdit(int id)
        {
            Product = _context.Products.FirstOrDefault(p => p.Id == id);
            if (Product == null)
            {
                return RedirectToPage("Index", new { info = "Product not found!" });
            }

            Products = _context.Products.ToList();
            return Page();
        }

        public IActionResult OnPostDelete(int id)
        {
            var productToDelete = _context.Products.FirstOrDefault(p => p.Id == id);
            if (productToDelete != null)
            {
                _context.Products.Remove(productToDelete);
                _context.SaveChanges();
                Info = $"{productToDelete.Name} deleted successfully!";
            }

            return RedirectToPage("Index", new { info = Info });
        }
    }
}
