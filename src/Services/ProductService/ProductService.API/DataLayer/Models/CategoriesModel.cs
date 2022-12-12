using System.ComponentModel.DataAnnotations;

namespace ProductService.API.DataLayer.Models
{
    public class CategoriesModel:EntityBase
    {
        public CategoriesModel()
        {
            Products = new HashSet<Products>();
        }
        [Key]
        public int CategoryId { get; set; }
        [Required]
        [MaxLength(20)]
        public string CategoryName { get; set; }
        [Required]
        [MaxLength(20)]
        public string SubCategory { get; set; }
        public string HSNcode { get; set; }

        public  ICollection<Products> Products { get; set; }
    }
}
