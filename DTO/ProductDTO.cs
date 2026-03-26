namespace RestaurantAPI.DTO
{
    public class ProductDTO
    {
        public int ProductId { get; set; }
        public string ProductName  { get; set; }=null!;
        public decimal Price { get; set; }
        public decimal UnitOnCost { get; set; }
        public int UnitOnStock { get; set; } 
        public int CategoryId{get;set;}  
    }
}
