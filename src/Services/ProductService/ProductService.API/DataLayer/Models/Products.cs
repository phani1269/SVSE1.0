﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductService.API.DataLayer.Models
{
    public class Products : EntityBase
    {
        [Key]
        public int ProductId { get; set; }
        [Required]
        [MaxLength(50)]
        public string? ProductName { get; set; }
        [Required]
        [MaxLength(20)]
        public string? ProductCode { get; set; }
        [Required]
        public int CapacityRating { get; set; }
        [Required]
        public int RetailPrice { get; set; }
        [Required]
        public int CostPrice { get; set; }
        [Required]
        public string? Warranty { get; set; }
        [ForeignKey("CategoryId")]
        public int CategoryId { get; set; }
        public  CategoriesModel Categories { get; set; }
    }
}
