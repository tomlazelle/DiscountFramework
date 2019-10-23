using System.Collections.Generic;
using System.Linq;

namespace DiscountFramework
{
    public class DiscountCart
    {
        public int CartId { get; set; }
        public decimal Discount { get; set; }
        public decimal OrignalTotal => GetOriginalAmount();
        public decimal DiscountedTotal => GetDiscountedAmount();
        public decimal DiscountedSubTotal => GetDiscountedSubTotal();
        public decimal OriginalSubTotal => GetOriginalSubTotal();
        public decimal OriginalShippingAmount { get; set; }
        public decimal DiscountedShippingAmount { get; set; }

        public decimal Tax { get; set; }

        public IEnumerable<DiscountItem> DiscountItems { get; set; }

        private decimal GetDiscountedSubTotal()
        {
            return DiscountItems.Sum(x => x.DiscountedAmount ?? x.Amount);
        }

        private decimal GetOriginalSubTotal()
        {
            return DiscountItems.Sum(x => x.Amount);
        }

        private decimal GetDiscountedAmount()
        {
            var discountedTotal = GetDiscountedSubTotal() - Discount;

            var taxed = discountedTotal*(1 + Tax);

            return taxed;
        }

        private decimal GetOriginalAmount()
        {
            var originalSubTotal = GetOriginalSubTotal();

            var taxed = originalSubTotal*(1 + Tax);

            return taxed;
        }
    }
}