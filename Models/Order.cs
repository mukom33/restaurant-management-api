using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace RestaurantAPI.Models
{
    
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        public int TableId { get; set; }
        public int EmployeeId { get; set; }
        public Table Table{get;set;}=null!;
        public Employee Employee{get;set;}=null!;
        public DateTime OrderDate { get; set; }
        public bool IsDelete { get; set; }
        public List<OrderDetail>OrderDetails=new ();
    }
}