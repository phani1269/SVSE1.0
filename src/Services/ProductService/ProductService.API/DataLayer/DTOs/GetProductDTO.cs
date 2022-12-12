﻿namespace ProductService.API.DataLayer.DTOs
{
    public class GetProductDTO
    {
        public int ProductId { get; set; }
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

        public List<ProductItemDTO> ProductItems { get; set; }

    }
    public class ProductItemDTO
    {
        public int ItemsId { get; set; }
        public string? ItemCode { get; set; }
    }
}
