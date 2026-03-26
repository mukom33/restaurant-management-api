using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Models;
using RestaurantAPI.Repository;

namespace RestaurantAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly IGenericRepository<Customer> _repository;
        public CustomerController(IGenericRepository<Customer> repository)
        {
            _repository=repository;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetCustomer()
        {
            var customers = await _repository.GetAllAsync();
            return Ok(customers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomer(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _repository.GetAsync(i=>i.CustomerId == id);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> CreateCustomer(Customer entity)
        {
            await _repository.AddAsync(entity);
            await _repository.SaveAsync();
            return CreatedAtAction(nameof(GetCustomer),new{id = entity.CustomerId},entity);
        }

        [HttpPut]
        public async Task<IActionResult>UpdateCustomer(int id,Customer entity)
        {
            if (id != entity.CustomerId)
            {
                return BadRequest();
            }
            var customer = await _repository.GetAsync(i => i.CustomerId == id);
            if (customer == null)
            {
                return NotFound();
            }
            
            customer.CustomerName = entity.CustomerName;
            customer.Phone = entity.Phone;
            try
            {
                await _repository.SaveAsync();
            }
            catch (Exception)
            {
                return NotFound();
            }
            
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult>DeleteCustomer(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var customer = await _repository.GetAsync(i => i.CustomerId == id);
            if (customer == null)
            {
                return NotFound();
            }

            _repository.Remove(customer);
            try
            {
                await _repository.SaveAsync();
            }
            catch (Exception)
            {
                return NotFound();
            }

            return NoContent();
        }        
    }
}