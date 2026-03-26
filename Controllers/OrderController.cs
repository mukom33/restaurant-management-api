using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Models;
using RestaurantAPI.Repository;

namespace RestaurantAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IGenericRepository<Order> _repository;
        public OrderController(IGenericRepository<Order> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            var orders = await _repository.GetAllAsync();
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult>GetOrder(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _repository.GetAsync(i=>i.OrderId==id);
            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        [HttpPost]
        public async Task<IActionResult>CreateOrder(Order entity)
        {
            await _repository.AddAsync(entity);
            await _repository.SaveAsync();
            return CreatedAtAction(nameof(GetOrder),new{id=entity.OrderId},entity);
        }

        [HttpPut]
        public async Task<IActionResult>UpdateOrder(int? id,Order entity)
        {
            if (id != entity.OrderId)
            {
                return BadRequest();
            }

            var order = await _repository.GetAsync(i=>i.OrderId==id);
            if (order == null)
            {
                return NotFound();
            }

            order.EmployeeId=entity.EmployeeId;
            order.TableId=entity.TableId;
            order.OrderDate=entity.OrderDate;
            order.IsDelete=entity.IsDelete;
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
        public async Task<IActionResult>DeleteOrder(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _repository.GetAsync(i=>i.OrderId==id);
            if (order == null)
            {
                return NotFound();
            }

            _repository.Remove(order);
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