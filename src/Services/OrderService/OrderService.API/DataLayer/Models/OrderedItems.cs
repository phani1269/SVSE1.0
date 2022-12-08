using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderService.API.DataLayer.Models
{
    public class OrderedItems:EntityBase
    {
        [Key]
        public int ItemsId { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int OrderedQuantity { get; set; }
        [Required]
        public decimal ItemAmount { get; set; }
        [Required]
        public decimal TotalAmount
        {
            get
            {
                decimal total = ItemAmount * OrderedQuantity;
                return total;
            }
        }
        [ForeignKey("OrderId")]
        public int OrderId { get; set; }
        public Order Orders { get; set; }

    }
}
