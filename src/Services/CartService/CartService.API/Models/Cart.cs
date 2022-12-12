namespace CartService.API.Models
{
    public class Cart : EntityBase
    {
        public int CartId { get; set; }
        public decimal TotalPrice
        {
            get
            {
                decimal total = 0;
                foreach (var item in Items)
                {
                    total += item.TotalAmount;
                }
                return total;
            }
        }
        public List<CartItem> Items { get; set; }
    }
}
