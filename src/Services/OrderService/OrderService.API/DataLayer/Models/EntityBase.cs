namespace OrderService.API.DataLayer.Models
{
    public class EntityBase
    {
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public string? ModifiedBy { get; set; }
    }
}
