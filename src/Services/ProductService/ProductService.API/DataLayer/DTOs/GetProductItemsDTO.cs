using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProductService.API.DataLayer.DTOs
{
    public class GetProductItemDTO
    {
        public int ItemsId { get; set; }
        public string? ItemCode { get; set; }
        public int ProductId { get; set; }
    }
    public class ProductDTO
    {
        public string? ProductName { get; set; }
        public string? ProductCode { get; set; }
        public int CapacityRating { get; set; }
        public int RetailPrice { get; set; }
        public int CostPrice { get; set; }
        public string? Warranty { get; set; }
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public string? SubCategory { get; set; }
        public string HSNcode { get; set; }

    }

    public class ItemDTO
    {
        public GetProductItemDTO ProductItem { get; set; }
        public ProductDTO product { get; set; }
    }


}
