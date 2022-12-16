using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OrderService.API.DataLayer.DTOs
{
    public class GetItemsDTO
    {
        public int ItemsId { get; set; }
        public string Itemcode { get; set; }
        public decimal CGST { get; set; }
        public decimal SGST { get; set; }
        public int OrderedQuantity { get; set; }
        public decimal ItemAmount { get; set; }
        public decimal TaxableAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public int OrderId { get; set; }
    }
}
