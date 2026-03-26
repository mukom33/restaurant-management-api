using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace RestaurantAPI.Models
{
    
    public class Booking
    {
        public int BookingId { get; set; }
        public int CustomerId{get;set;}
        public Customer Customer{get;set;} = null!;
        public DateTime DateOfBooking  { get; set; }
        public int TableId { get; set; }
        public Table Table{get;set;}=null!;
        public bool IsDelete { get; set; }     
    }
}
