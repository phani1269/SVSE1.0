namespace OrderService.API.DataLayer.Models
{
    public class ResponseModel
    {
        public bool IsSuccess { get; set; } = true;
        public object? Result { get; set; }
        public string? DisplayMessage { get; set; } = "Success";
        public List<string>? ErrorMessages { get; set; }
    }
}
