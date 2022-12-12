namespace CartService.API.Models
{
    public class CartItem : EntityBase
    {
        public int CartItemId { get; set; }
        public string? ItemCode { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal ItemPrice { get; set; }
        public decimal TotalAmount
        {
            get
            {
                decimal total = ItemPrice * Quantity;
                return total;
            }
        }
    }
}
