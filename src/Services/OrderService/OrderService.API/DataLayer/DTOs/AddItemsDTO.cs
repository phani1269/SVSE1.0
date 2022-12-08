using System.ComponentModel.DataAnnotations;

namespace OrderService.API.DataLayer.DTOs
{
    public class AddItemsDTO
    {
        public int ProductId { get; set; }
        public int OrderedQuantity { get; set; }
        public decimal ItemAmount { get; set; }
    }
}
