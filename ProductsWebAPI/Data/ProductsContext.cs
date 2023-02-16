using Microsoft.EntityFrameworkCore;
using ProductsWebAPI.Models;

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
        /*protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Product>()
            //   .HasOne(p => p.Category)
           //    .WithMany(c => c.Fields)
          //     .HasForeignKey(p => p.CategoryId)
          //     .OnDelete(DeleteBehavior.Restrict);

        }*/
    }
}
