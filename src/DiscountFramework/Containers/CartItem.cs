using System.Collections.Generic;

namespace DiscountFramework.Containers
{
    public class CartItem
    {
        public int Quantity { get; set; }
        public decimal Amount { get; set; }
        public string SKU { get; set; }
        public bool Taxable { get; set; }
        public List<string> KitSKUList { get; set; }
    }
}