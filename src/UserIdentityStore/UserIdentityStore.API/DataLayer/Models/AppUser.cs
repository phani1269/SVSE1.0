using Microsoft.AspNetCore.Identity;

namespace UserIdentityStore.API.DataLayer.Models
{
    public class AppUser : IdentityUser
    {
        public string? FirstName { get; set; } 
        public string? LastName { get; set; } 
        public string? Address { get; set; }  
        public string? CreatedAt { get; set; }  
    }
}
