using Mango.Services.ProductAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.ProductAPI.Data
{
    public class AppDbContext : DbContext
    {
      
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {
            
        }
        public DbSet<Product> Products { get; set; }

        protected override void  OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>().HasData(new Product

            {
                    ProductId = 1,
                    CategoryName = "Starter",
                    Name = "Samosa",
                    Price = 3.99,
                    ImageUrl = "https://placehold.co/602x402"
            });

            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 2,
                CategoryName = "Main Course",
                Name = "Paav Bhaji",
                Price = 12,
                ImageUrl = "https://placehold.co/602x402"
            });

            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 3,
                CategoryName = "Desert",
                Name = "Ice Cream",
                Price = 4.99,
                ImageUrl = "https://placehold.co/602x402"
            });


        }
    }
}
