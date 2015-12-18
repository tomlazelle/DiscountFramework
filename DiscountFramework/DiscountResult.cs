using System.Collections.Generic;

namespace DiscountFramework
{
    public class DiscountCart
    {
        public int CartId { get; set; }
        public decimal Discount { get; set; }
        public decimal DiscountedAmount { get; set; }
        public decimal OrignalTotal { get; set; }
        public decimal OriginalSubTotal { get; set; }
        public decimal DiscountedTotal { get; set; }
        public decimal DiscountedSubTotal { get; set; }
        public decimal Tax { get; set; }

        public IEnumerable<DiscountItem> DiscountItems { get; set; }
    }

    public class DiscountItem
    {
        public int CartItemId { get; set; }
        public int ProductId { get; set; }
        public decimal Amount { get; set; }
        public decimal DiscountedAmount { get; set; }
        public decimal Discount { get; set; }
    }
}