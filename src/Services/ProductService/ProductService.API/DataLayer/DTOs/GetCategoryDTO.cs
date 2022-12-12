namespace ProductService.API.DataLayer.DTOs
{
    public class GetCategoryDTO
    {
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public string? SubCategory { get; set; }
        public string HSNcode { get; set; }

    }
}
