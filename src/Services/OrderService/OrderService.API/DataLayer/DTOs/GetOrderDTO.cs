using System.ComponentModel.DataAnnotations;

namespace OrderService.API.DataLayer.DTOs
{
    public class GetOrderDTO
    {
        public int OrderId { get; set; }
        public string? CustomerName { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public decimal PayableAmount { get; set; }
        public bool PaymentConfirmation { get; set; } 
        public List<GetOrderDTO> OrderdItems { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public string? ModifiedBy { get; set; }
    }
}
