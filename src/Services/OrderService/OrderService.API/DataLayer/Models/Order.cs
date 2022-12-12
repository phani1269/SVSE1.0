using System.ComponentModel.DataAnnotations;

namespace OrderService.API.DataLayer.Models
{
    public class Order:EntityBase
    {
        public Order()
        {
            OrderedItemsList = new HashSet<OrderedItems>();
        }
        [Key]
        public int OrderId { get; set; }
        [Required]
        public string? CustomerName { get; set; }
        [Required]
        public string? Address { get; set; }
        [Required]
        public string? PhoneNumber { get; set; }
        [Required]
        public string? GSTNumber { get; set; }
        [Required]
        public string? VehicleNumber { get; set; }    
        public decimal PayableAmount { get; set; }    
        public bool PaymentConfirmation { get; set; } = false;
        public ICollection<OrderedItems> OrderedItemsList { get; set; }  
    }
}
