using Microsoft.EntityFrameworkCore;
using OrderService.API.DataLayer.Models;

namespace OrderService.API.DataLayer.Contexts
{
    public class OrderDbContext : DbContext
    {
        public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options)
        {

        }
        public DbSet<Order> orders { get; set; }
        public DbSet<OrderedItems> orderedItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderedItems>(entity =>
            {
                entity.HasOne(c => c.Orders)
                        .WithMany()
                        .HasForeignKey(c => c.OrderId)
                        .HasConstraintName("fk_OrderId");
            });
        }
    }
}
