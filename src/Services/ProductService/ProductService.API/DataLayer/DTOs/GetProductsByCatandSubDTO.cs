namespace ProductService.API.DataLayer.DTOs
{
    public class GetProductsByCatandSubDTO
    {
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public string? SubCategory { get; set; }
        public List<ProductsList> ProductsList { get; set; }
    }
    public class ProductsList
    {
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public string? ProductCode { get; set; }
        public int CapacityRating { get; set; }
        public int RetailPrice { get; set; }
        public int CostPrice { get; set; }
        public string? Warranty { get; set; }
        public List<ProductItemDTO> ProductItems { get; set; }

    }
}
