using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Models;
using RestaurantAPI.Repository;

namespace RestaurantAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderDetailController : ControllerBase
    {
       private readonly IGenericRepository<OrderDetail> _repository;
       public OrderDetailController(IGenericRepository<OrderDetail> repository)
        {
            _repository=repository;
        }

       [HttpGet]
       public async Task<IActionResult> GetOrderDetails()
        {
            var orderdetails = await _repository.GetAllAsync();
            return Ok(orderdetails);
        }
       
       [HttpGet("{id}")]
       public async Task<IActionResult> GetOrderDetail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            } 

            var orderdetail=await _repository.GetAsync(i=>i.OrderDetailId==id);
            if(orderdetail==null)
            {
                return NotFound();
            }
            
            return Ok(orderdetail);
        }

        [HttpPost]
        public async Task<IActionResult>CreateOrderDetail(OrderDetail entity)
        {
            await _repository.AddAsync(entity);
            await _repository.SaveAsync();
            return CreatedAtAction(nameof(GetOrderDetail),new{id=entity.OrderDetailId},entity);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult>UpdateOrderDetail(int? id,OrderDetail entity)
        {
            if (id != entity.OrderDetailId)
            {
                return BadRequest();
            }

            var Orderdetail = await _repository.GetAsync(i=>i.OrderDetailId==id);
            if (Orderdetail == null)
            {
                return NotFound();
            }

            Orderdetail.OrderId=entity.OrderId;
            Orderdetail.ProductId=entity.ProductId;
            Orderdetail.Quantity=entity.Quantity;
            try
            {
                await _repository.SaveAsync();
            }
            catch(Exception)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}