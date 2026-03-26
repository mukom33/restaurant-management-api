using System.ComponentModel.DataAnnotations;

namespace RestaurantAPI.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; } 
        public string CustomerName { get; set; }=null!;
        public string Phone { get; set; }=null!;
        public List<Booking>Bookings{get;set;}=new();
    }
}