using System.ComponentModel.DataAnnotations;

namespace RestaurantAPI.Models
{
    public class Table
    {
        [Key]
        public int TableId { get; set; }

        public bool IsDelete { get; set; }
        public List<Order>Orders{get;set;}=new();
        public List<Booking>Bookings{get;set;}=new();
    }
}