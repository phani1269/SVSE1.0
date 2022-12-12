using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductService.API.DataLayer.Models
{
    public class ProductItemsModel
    {
        [Key]
        public int ItemsId { get; set; }
        [Required]
        public string? ItemCode { get; set; }
        [ForeignKey("ProductId")]
        public int ProductId { get; set; }
        public Products Product { get; set; }
    }
}
