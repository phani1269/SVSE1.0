using System.ComponentModel.DataAnnotations;

namespace ComplaintService.API.DataLayer.Models
{
    public class ComplaintModel: BaseModel
    {
        [Key]
        public int Id { get; protected set; }
        public string? UserName { get; set; }
        public string? MobileNumber { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
    }
}
