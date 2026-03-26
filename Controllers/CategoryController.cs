using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Models;
using RestaurantAPI.Repository;

namespace RestaurantAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly IGenericRepository<Category> _repository;
        public CategoryController(IGenericRepository<Category> repository)
        {
            _repository=repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategory()
        {
            var categories = await _repository.GetAllAsync();
            return Ok(categories);           
        }

       [HttpGet("{id}")]
       public async Task<IActionResult> GetCategory(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var category = await _repository.GetAsync(i => i.CategoryId == id);
            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult>CreateCategory(Category entity)
        {
            await _repository.AddAsync(entity);
            await _repository.SaveAsync();
            return CreatedAtAction(nameof(GetCategory),new {id = entity.CategoryId},entity);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult>UpdateCategory(int? id,Category entity)
        {
            if (id != entity.CategoryId)
            {
                return BadRequest();
            }
            var category = await _repository.GetAsync(i => i.CategoryId == id);
            if (category == null)
            {
                return NotFound();
            }

            category.CategoryName = entity.CategoryName;
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
        public async Task<IActionResult>DeleteCategory(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var category = await _repository.GetAsync(i=>i.CategoryId==id);

            if (category == null)
            {
                return NotFound();
            }

            _repository.Remove(category);
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