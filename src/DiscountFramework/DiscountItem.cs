using System.Collections.Generic;

namespace DiscountFramework
{
    public class DiscountItem
    {
        public string CartItemId { get; set; }
        public decimal? DiscountedAmount { get; set; } = 0;
        public decimal? Discount { get; set; } = 0;
        public int Quantity { get; set; } = 0;
        public decimal Amount { get; set; } = 0;
        public string SKU { get; set; }
        public bool Taxable { get; set; }
        public List<string> KitSKUList { get; set; }
    }
}