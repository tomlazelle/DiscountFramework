namespace DiscountFramework
{
    public class DiscountItem
    {
        public int CartItemId { get; set; }
        public int ProductId { get; set; }
        public decimal Amount { get; set; }
        public decimal? DiscountedAmount { get; set; }
        public decimal? Discount { get; set; }
        public int Quantity { get; set; }
    }
}