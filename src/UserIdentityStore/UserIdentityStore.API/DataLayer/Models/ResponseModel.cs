namespace UserIdentityStore.API.DataLayer.Models
{
    public class ResponseModel
    {
        public int StatusCode { get; set; }
        public string Status { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
    }
}
