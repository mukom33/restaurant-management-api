using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Models;
using RestaurantAPI.Repository;

namespace RestaurantAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingController : ControllerBase
    {
       private readonly IGenericRepository<Booking> _repository;
    
       public BookingController(IGenericRepository<Booking> repository)
       {
            _repository = repository;    
       }

      [HttpGet]
      public async Task<IActionResult> GetBooking()
       {
          var customers = await _repository.GetAllAsync();
          return Ok(customers);

       }

      [HttpGet("{id}")]
      public async Task<IActionResult>GetBooking(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _repository.GetAsync(i=>i.BookingId == id);
            if (booking == null)
            {
                return NotFound();
            }

            return Ok(booking);
        }

        [HttpPost]
        public async Task<IActionResult>CreateBooking(Booking entity)
        {
            await _repository.AddAsync(entity);
            await _repository.SaveAsync();
            return CreatedAtAction(nameof(GetBooking),new {id = entity.BookingId } ,entity);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult>UpdateBooking(int? id,Booking entity)
        {
            if (id != entity.BookingId)
            {
                return BadRequest();
            }

            var Booking = await _repository.GetAsync(i => i.BookingId == id);
            if (Booking == null)
            {
                return NotFound();
            }

            Booking.CustomerId = entity.CustomerId;
            Booking.DateOfBooking = entity.DateOfBooking;
            Booking.IsDelete = entity.IsDelete;
            Booking.TableId = entity.TableId;
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
        public async Task<IActionResult>DeleteBooking(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Booking = await _repository.GetAsync(i => i.BookingId == id);
            if (Booking == null)
            {
                return NotFound();
            }

            _repository.Remove(Booking);
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