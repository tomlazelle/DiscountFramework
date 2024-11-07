using System.Collections.Generic;

namespace DiscountFramework.Containers
{
    public class Cart
    {
        public string TenantId { get; set; }
        public string CouponCode { get; set; }
        public decimal TaxRate { get; set; }
        public List<CartItem> Items = new();
    }
}