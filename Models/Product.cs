using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace RestaurantAPI.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        
        public string ProductName  { get; set; }=null!;
        public decimal Price { get; set; }
        public decimal UnitOnCost { get; set; }
        public int UnitOnStock { get; set; } 
        public int CategoryId{get;set;}  
        public Category Category{get;set;}=null!;
        public List<OrderDetail>OrderDetails=new ();    
    }
}