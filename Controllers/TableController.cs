using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Models;
using RestaurantAPI.Repository;

namespace RestaurantAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TableController : ControllerBase
    {
        private readonly IGenericRepository<Table> _repository;
        public TableController(IGenericRepository<Table> repository)
        {
            _repository=repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetTables()
        {
            var tables = await _repository.GetAllAsync();
            return Ok(tables);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult>GetTable(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var table = await _repository.GetAsync(i => i.TableId == id);
            if (table == null)
            {
                return NotFound();
            }

            return Ok(table);
        }

        [HttpPost]
        public async Task<IActionResult>CreateTable(Table entity)
        {
            await _repository.AddAsync(entity);
            await _repository.SaveAsync();
            return CreatedAtAction(nameof(GetTable),new{id = entity.TableId});
        }

        [HttpPut("{id}")]
        public async Task<IActionResult>UpdateTable(int? id,Table entity)
        {
            if (id == entity.TableId)
            {
                return BadRequest();
            }

            var tables = await _repository.GetAsync(i=>i.TableId==id);
            if (tables == null)
            {
                return NotFound();
            }

            tables.IsDelete=entity.IsDelete;
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
        public async Task<IActionResult>DeleteTable(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var table = await _repository.GetAsync(i=>i.TableId==id);
            if (table == null)
            {
                return NotFound();
            }

            _repository.Remove(table);
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