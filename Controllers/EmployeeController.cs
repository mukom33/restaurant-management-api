using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Models;
using RestaurantAPI.Repository;

namespace RestaurantAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IGenericRepository<Employee> _repository;
        public EmployeeController(IGenericRepository<Employee> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployee()
        {
            var employees = await _repository.GetAllAsync();
            return Ok(employees);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployee(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _repository.GetAsync(i=>i.EmployeeId == id);
            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }

        [HttpPost]
        public async Task<IActionResult>CreateEmployee(Employee entity)
        {
            await _repository.AddAsync(entity);
            await _repository.SaveAsync();
            return CreatedAtAction(nameof(GetEmployee),new{id = entity.EmployeeId},entity);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult>UpdateEmployee(int? id,Employee entity)
        {
            if (id != entity.EmployeeId)
            {
                return BadRequest();
            }

            var employee = await _repository.GetAsync(i=>i.EmployeeId == id);
            if (employee == null)
            {
                return NotFound();
            }

            employee.FirstName = entity.FirstName;
            employee.LastName = entity.LastName;
            employee.Age = entity.Age;
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
        public async Task<IActionResult>DeleteEmployee(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _repository.GetAsync(i=>i.EmployeeId == id);
            if (employee == null)
            {
                return NotFound();
            }

            _repository.Remove(employee);
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