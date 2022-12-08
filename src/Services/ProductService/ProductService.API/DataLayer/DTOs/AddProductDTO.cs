namespace ProductService.API.DataLayer.DTOs
{
    public class AddProductDTO
    {
        public string? ProductName { get; set; }
        public string? ProductCode { get; set; }
        public int CapacityRating { get; set; }
        public int RetailPrice { get; set; }
        public int CostPrice { get; set; }
        public string? Warranty { get; set; }
        public int CategoryId { get; set; }
        public string? ModifiedBy { get; set; }

    }
}
