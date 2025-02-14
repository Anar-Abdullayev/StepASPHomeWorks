using ECommerce.DTOs;
using ECommerce.Entities;
using ECommerce.Repository.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(IProductRepository _context) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> Get([FromQuery] string sortBy = null, [FromQuery] SortOrder sortOrder = SortOrder.Ascending)
        {
            var result = await _context.GetAll(sortBy: sortBy, sortOrder: sortOrder);
            

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _context.Get(p => p.Id == id);

            if (product is null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<UpdateProductDto>> UpdateProduct(int id, UpdateProductDto updatedProduct)
        {
            var product = await _context.Get(p => p.Id == id);

            if (product is null)
            {
                return NotFound();
            }

            product.Name = updatedProduct.Name;
            product.Price = updatedProduct.Price;
            product.Discount = updatedProduct.Discount;

            await _context.Update(product);

            return Ok(updatedProduct);
        }

        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(AddProductDto product)
        {
            var newProduct = new Product
            {
                Name = product.Name,
                Price = product.Price,
                Discount = product.Discount,
            };
            var addedProduct = await _context.Add(newProduct);

            return Created();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var customer = await _context.Get(p => p.Id == id);

            if (customer == null)
            {
                return NotFound();
            }

            await _context.Delete(customer);

            return NoContent();
        }

    }
}
