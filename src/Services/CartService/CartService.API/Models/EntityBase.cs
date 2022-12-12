namespace CartService.API.Models
{
    public class EntityBase
    {
        public DateTime CreatedAt { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
