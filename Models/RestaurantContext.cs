using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace RestaurantAPI.Models
{
    public class RestaurantContext : IdentityDbContext<AppUser,AppRole,int>
    {
        public RestaurantContext(DbContextOptions<RestaurantContext> options):base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Category>().HasData(
                new Category() { CategoryId = 1, CategoryName = "Soğuk İçecek" },
                new Category() { CategoryId = 2, CategoryName = "Ana Yemek" }
            );

            modelBuilder.Entity<Customer>().HasData(
                new Customer() { CustomerId = 1, CustomerName = "Muhammed", Phone = "1231521" },
                new Customer() { CustomerId = 2, CustomerName = "Çınar", Phone = "0555555" }
            );

            modelBuilder.Entity<Product>().HasData(
                new Product() { ProductId = 1, ProductName = "elma", Price = 14, UnitOnCost = 8, UnitOnStock = 30, CategoryId = 1 },
                new Product() { ProductId = 2, ProductName = "elma", Price = 15, UnitOnCost = 6, UnitOnStock = 5, CategoryId = 1 },
                new Product() { ProductId = 3, ProductName = "elma", Price = 20, UnitOnCost = 10, UnitOnStock = 20, CategoryId = 2 },
                new Product() { ProductId = 4, ProductName = "elma", Price = 30, UnitOnCost = 15, UnitOnStock = 50, CategoryId = 2 }
            );
            
            
            modelBuilder.Entity<Product>().HasOne(p=>p.Category).WithMany(c=>c.Products).HasForeignKey(p=>p.CategoryId);
            
            modelBuilder.Entity<Order>().HasOne(o=>o.Employee).WithMany(e=>e.Orders).HasForeignKey(o=>o.EmployeeId);
            modelBuilder.Entity<Order>().HasOne(o=>o.Table).WithMany(t=>t.Orders).HasForeignKey(o=>o.TableId);

            modelBuilder.Entity<Booking>().HasOne(b=>b.Customer).WithMany(c=>c.Bookings).HasForeignKey(b=>b.CustomerId);
            modelBuilder.Entity<Booking>().HasOne(b=>b.Table).WithMany(t=>t.Bookings).HasForeignKey(b=>b.TableId);

            modelBuilder.Entity<OrderDetail>().HasOne(od=>od.Product).WithMany(p=>p.OrderDetails).HasForeignKey(o=>o.ProductId);
            modelBuilder.Entity<OrderDetail>().HasOne(od=>od.Order).WithMany(o=>o.OrderDetails).HasForeignKey(od=>od.OrderId);        
        }
       
        public DbSet<Product>Products{get;set;}=null!;
        public DbSet<Customer>Customers{get;set;}=null!;
        public DbSet<Category>Categories{get;set;}=null!;
        public DbSet<Booking>Bookings{get;set;}
        public DbSet<Table>Tables{get;set;}
        public DbSet<Employee>Employees{get;set;}
        public DbSet<Order>Orders{get;set;}
        public DbSet<OrderDetail>OrderDetails{get;set;}
        public DbSet<AppRole>AppRoles{get;set;}
        public DbSet<AppUser>AppUsers{get;set;}

    }
}
