using Microsoft.EntityFrameworkCore;
using ProductsWebAPI.ApplicationCore.Entities;

namespace ProductsWebAPI.Data
{
    public class ProductsContext : DbContext
    {
        public ProductsContext(DbContextOptions<ProductsContext> options) : base(options)
        {
            
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductField> ProductFields { get; set; }
        public DbSet<ProductFieldValue> ProductFieldValues { get; set; }
        
    }
}
