using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.DTO;
using RestaurantAPI.Models;
using RestaurantAPI.Repository;

namespace RestaurantAPI.Controllers
{
   [ApiController]
   [Route("api/[controller]")]
   public class ProductController : ControllerBase
   {
      
      private IGenericRepository<Product> _repository;
      public ProductController(IGenericRepository<Product> repository)
      {
         _repository = repository;
      }
         
      [HttpGet]
      public async Task<IActionResult> GetProducts()
      {
         var products = await _repository.GetAllAsync();
         var result = products.Select(p => ProductToDTO(p)).ToList();
         return Ok(result);           
      }
      
      [Authorize]
      [HttpGet("{id}")]
      public async Task<IActionResult> GetProduct(int? id)
      {         
         if (id == null)
         {
            return NotFound();
         }

         var products = await _repository.GetAsync(p=>p.ProductId==id);
         if (products == null)
         {
            return NotFound();    
         }

         var result = ProductToDTO(products); 
         return Ok(result);
      }

      [HttpPost]
      public async Task<IActionResult>CreateProduct(Product entity)
      {
         await _repository.AddAsync(entity);
         await _repository.SaveAsync();
         return CreatedAtAction(nameof(GetProduct),new{id = entity.ProductId},entity);
      }

      [HttpPut("{id}")]
      public async Task<IActionResult>UpdateProduct(int? id,Product entity)
      {
         if (id != entity.ProductId)
         {
            return BadRequest();
         }

         var product = await _repository.GetAsync(i => i.ProductId == id);
         if (product == null)
         {
            return NotFound();
         }

         product.ProductName=entity.ProductName;
         product.Price=entity.Price;
         product.UnitOnCost=entity.UnitOnCost;
         product.UnitOnStock=entity.UnitOnStock;
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
      public async Task<IActionResult> DeleteProduct(int? id)
      {
         if (id == null)
         {
            return NotFound();
         }
            
         var product = await _repository.GetAsync(i => i.ProductId == id);

         if (product == null)
         {
            return NotFound();
         }

         _repository.Remove(product);
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
      private static ProductDTO ProductToDTO(Product p)
      {
         var entity = new ProductDTO();
         return new ProductDTO
         {
            ProductId = p.ProductId,
            ProductName = p.ProductName,
            Price = p.Price,
            CategoryId = p.CategoryId,
            UnitOnCost = p.UnitOnCost,
            UnitOnStock = p.UnitOnStock
         };
      }    
   }  
}
