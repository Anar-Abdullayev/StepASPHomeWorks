using ECommerce.DTOs;
using ECommerce.Entities;
using ECommerce.Repository.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController(IOrdersRepository _context, ICustomerRepository customerRepository, IProductRepository productRepository) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> Get()
        {
            var result = await _context.GetAll();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            var order = await _context.Get(o => o.Id == id);

            if (order is null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<UpdateOrderDto>> UpdateOrder(int id, UpdateOrderDto updatedOrder)
        {
            var order = await _context.Get(o => o.Id == id);

            if (order is null)
            {
                return NotFound();
            }

            var product = await productRepository.Get(p => p.Id == updatedOrder.ProductId);
            if (product is null)
                return NotFound();

            var customer = await customerRepository.Get(c => c.Id == updatedOrder.CustomerId);
            if (customer is null)
                return NotFound();

            

            order.OrderDate = updatedOrder.OrderDate;
            order.ProductId = updatedOrder.ProductId;
            order.CustomerId = updatedOrder.CustomerId;

            await _context.Update(order);

            return Ok(updatedOrder);
        }

        [HttpPost]
        public async Task<ActionResult<Order>> PostOrders(AddOrderDto order)
        {
            var product = await productRepository.Get(p => p.Id == order.ProductId);
            if (product is null)
                return NotFound();

            var customer = await customerRepository.Get(c => c.Id == order.CustomerId);
            if (customer is null)
                return NotFound();

            var newOrder = new Order
            {
                OrderDate = DateTime.Now,
                ProductId = order.ProductId,
                CustomerId = order.CustomerId
            };
            var addedOrder = await _context.Add(newOrder);

            return Created();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteOrder(int id)
        {
            var order = await _context.Get(o => o.Id == id);

            if (order == null)
            {
                return NotFound();
            }

            await _context.Delete(order);

            return NoContent();
        }
    }
}
