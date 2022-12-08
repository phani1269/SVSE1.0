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
        public decimal PayableAmount
        {
            get
            {
                decimal total = 0;
                foreach (var item in OrderedItemsList)
                {
                    total += item.TotalAmount;
                }
                return total;
            }
        }
        public bool PaymentConfirmation { get; set; } = false;
        public ICollection<OrderedItems> OrderedItemsList { get; set; }  
    }
}
