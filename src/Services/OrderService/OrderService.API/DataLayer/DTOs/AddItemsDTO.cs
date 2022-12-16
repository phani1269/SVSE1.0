using System.ComponentModel.DataAnnotations;

namespace OrderService.API.DataLayer.DTOs
{
    public class AddItemsDTO
    {
        public int OrderId { get; set; }
        public int Itemcode { get; set; }
        public int OrderedQuantity { get; set; }
        public decimal ItemAmount { get; set; }
        public decimal CGST { get; set; }
        public decimal SGST { get; set; }
        public decimal TaxableAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public string? ModifiedBy { get; set; }

    }
}
