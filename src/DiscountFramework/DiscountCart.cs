using System.Collections.Generic;
using System.Linq;
using CommandQuery.Framing;

namespace DiscountFramework
{
    public class DiscountCart
    {
        public int CartId { get; set; }
        public decimal Discount { get; set; }

        public string CouponCode { get; set; }

        public decimal Total => GetTotal();
        public decimal TotalWithTax => GetTotalWithTax();
        public decimal TotalWithTaxAndDiscount => GetTotalWithTaxAndDiscount();

        public decimal SubTotal => GetSubTotal();
        public decimal SubTotalWithTax => GetSubTotalWithTax();
        public decimal SubTotalWithTaxAndDiscount => GetSubTotalWithTaxAndDiscount();


        public decimal ShippingAmount { get; set; }
        public decimal ShippingAmountWithDiscount { get; set; }

        public decimal TaxRate { get; set; }

        public IEnumerable<DiscountItem> DiscountItems { get; set; }

        // Totals
        private decimal GetTotalWithTaxAndDiscount()
        {
            return GetSubTotalWithTaxAndDiscount() - Discount;
        }

        private decimal GetTotalWithTax()
        {
            return GetSubTotalWithTax();
        }
        private decimal GetTotal()
        {
            return GetSubTotal();
        }

        // Subtotals

        private decimal GetSubTotalWithTaxAndDiscount()
        {
            decimal result = 0;
            foreach (var x in DiscountItems)
            {
                var itemTotal = (x.DiscountedAmount ?? x.Amount) * x.Quantity;
                if (x.Taxable)
                {
                    result += itemTotal * (1 + TaxRate);
                }
                else
                {
                    result += itemTotal;
                }

            }

            return result;
        }

        private decimal GetSubTotalWithTax()
        {
            decimal result = 0;
            foreach (var x in DiscountItems)
            {
                var itemTotal = x.Amount * x.Quantity;
                if (x.Taxable)
                {
                    result += itemTotal * (1 + TaxRate);
                }
                else
                {
                    result += itemTotal;
                }
            }

            return result;
        }

        private decimal GetSubTotal()
        {
            decimal result = 0;
            foreach (var x in DiscountItems)
            {
                var itemTotal = x.Amount * x.Quantity;

                result += itemTotal;
            }

            return result;
        }

    }
}