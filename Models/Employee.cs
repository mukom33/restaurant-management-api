using System.ComponentModel.DataAnnotations;

namespace RestaurantAPI.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }

        public string  FirstName { get; set; }=null!;
        public string  LastName { get; set; }=null!;
        public int Age { get; set; }
        public List<Order>Orders=new ();
    }
}