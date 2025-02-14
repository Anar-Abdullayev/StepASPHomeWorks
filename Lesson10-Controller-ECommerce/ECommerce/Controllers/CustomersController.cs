using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ECommerce.Data;
using ECommerce.Entities;
using ECommerce.Repository.Abstract;
using ECommerce.DTOs;

namespace ECommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController(ICustomerRepository _context) : ControllerBase
    {

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> Get()
        {
            var result = await _context.GetAll();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
            var customer = await _context.Get(c => c.Id == id);

            if (customer is null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<UpdateCustomerDto>> UpdateCustomer(int id, UpdateCustomerDto updatedCustomer)
        {
            var customer = await _context.Get(c => c.Id == id);

            if (customer is null)
            {
                return NotFound();
            }

            customer.Name = updatedCustomer.Name;
            customer.Surname = updatedCustomer.Surname;

            await _context.Update(customer);

            return Ok(updatedCustomer);
        }

        [HttpPost]
        public async Task<ActionResult<Customer>> PostCustomer(AddCustomerDto customer)
        {
            var newCustomer = new Customer
            {
                Name = customer.Name,
                Surname = customer.Surname,
            };
            var addedCustomer = await _context.Add(newCustomer);

            return CreatedAtAction("GetCustomer", new { id = addedCustomer.Id }, customer);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var customer = await _context.Get(c=>c.Id ==  id);

            if (customer == null)
            {
                return NotFound();
            }

            await _context.Delete(customer);

            return NoContent();
        }

    }
}
