using System.ComponentModel.DataAnnotations;

namespace OrderService.API.DataLayer.DTOs
{
    public class updateOrderDTO
    {
        public int OrderId { get; set; }
        public string? CustomerName { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string? GSTNumber { get; set; }
        public string? VehicleNumber { get; set; }
        public decimal PayableAmount { get; set; }
        public bool PaymentConfirmation { get; set; } = true;
        public string? ModifiedBy { get; set; }

    }
}
