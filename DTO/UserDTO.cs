using System.ComponentModel.DataAnnotations;

namespace RestaurantAPI.DTO
{
    public class UserDTO
    {
        [Required]
        public string FullName { get; set; }=null!;
        [Required]
        public string Username { get; set; }=null!;
        [Required]
        public string Email { get; set; }=null!;  
        [Required]
        public string Password{get;set;}=null!;        
    }
}