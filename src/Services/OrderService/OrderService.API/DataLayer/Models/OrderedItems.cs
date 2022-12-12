using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderService.API.DataLayer.Models
{
    public class OrderedItems:EntityBase
    {
        [Key]
        public int ItemsId { get; set; }
        [Required]
        public int Itemcode { get; set; }
        [Required]
        public decimal CGST { get; set; }
        [Required]
        public decimal SGST { get; set; }
        [Required]
        public int OrderedQuantity { get; set; }
        [Required]
        public decimal ItemAmount { get; set; }
        [Required]
        public decimal TaxableAmount { get; set; }

        [Required]
        public decimal TotalAmount { get; set; }
        [ForeignKey("OrderId")]
        public int OrderId { get; set; }
        public Order Orders { get; set; }

    }
}
