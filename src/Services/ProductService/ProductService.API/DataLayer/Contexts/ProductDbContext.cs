using Microsoft.EntityFrameworkCore;
using ProductService.API.DataLayer.Models;

namespace ProductService.API.DataLayer.Contexts
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Products> Products { get; set; }
        public DbSet<CategoriesModel> Categories { get; set; }
        public DbSet<ProductItemsModel> ProductItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure entity and its properties using Fluent API

           
           
            modelBuilder.Entity<Products>(entity =>
            {
                //entity.HasAlternateKey(e => e.ProductName)
                      //.HasName("uq_ProductName");

                entity.HasOne(c => c.Categories)
                        .WithMany()
                        .HasForeignKey(c => c.CategoryId)
                        .HasConstraintName("fk_CatgeoryId");
            });
        }
    }
}
