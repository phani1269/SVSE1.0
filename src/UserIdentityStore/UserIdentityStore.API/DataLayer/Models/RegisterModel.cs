namespace UserIdentityStore.API.DataLayer.Models
{
    public class RegisterModel
    {
        public string UserName { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string CreatedAt { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
