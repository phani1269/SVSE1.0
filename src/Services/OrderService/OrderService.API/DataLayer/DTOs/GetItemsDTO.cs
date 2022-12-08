using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OrderService.API.DataLayer.DTOs
{
    public class GetItemsDTO
    {
        public int ItemsId { get; set; }
        public int ProductId { get; set; }
        public int OrderedQuantity { get; set; }
        public decimal ItemAmount { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
