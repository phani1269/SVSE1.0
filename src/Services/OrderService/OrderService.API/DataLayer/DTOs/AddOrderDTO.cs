using System.ComponentModel.DataAnnotations;

namespace OrderService.API.DataLayer.DTOs
{
    public class AddOrderDTO
    {
        public string? CustomerName { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string? GSTNumber { get; set; }
        public string? VehicleNumber { get; set; }
        public string? ModifiedBy { get; set; }

        //  public List<AddItemsDTO> Items { get; set; }
    }
}
