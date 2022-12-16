namespace OrderService.API.DataLayer.DTOs
{
    public class ItemCodeDTO
    {
        public string ItemCode { get; set; }
        public List<GetOrderDTO> Orders { get; set; }
    }
}
