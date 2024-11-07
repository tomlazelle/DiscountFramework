using System.Collections.Generic;
using System.Linq;

namespace DiscountFramework;

public class DiscountCart
{
    public int CartId { get; set; }

    // this is a percentage off the total
    public decimal DiscountPercentage { get; set; }

    // this is a dollar amount off the total
    public decimal DiscountDollars { get; set; }

    public string CouponCode { get; set; }


    /// <summary>
    ///     Gets the subtotal of all items with discounts applied.
    /// </summary>
    /// <value>
    ///     Subtotal of all items with discounts applied.
    /// </value>
    public decimal SubTotal => GetTotalWithDiscount();


    /// <summary>
    ///     Gets the subtotal of all items with tax applied.
    /// </summary>
    /// <value>
    ///     Total of all items with tax applied.
    /// </value>
    public decimal Total => GetTotal();

    public decimal TaxRate { get; set; }

    public List<DiscountItem> DiscountItems { get; set; }

    // Totals
    private decimal GetTotalWithDiscount()
    {
        var subTotalWithDiscount = DiscountItems.Sum(x =>
        {
            // Use DiscountedAmount if available; otherwise, default to Amount
            var itemTotal = (x.Amount - x.Discount) * x.Quantity;

            if (DiscountPercentage > 0)
            {
                var discountAmount = itemTotal * DiscountPercentage;
                itemTotal -= discountAmount;
            }

            // Apply tax if the item is taxable
            return itemTotal;
        });


        // Apply global cart discount once on the total if intended
        return subTotalWithDiscount.Value;
    }

    private decimal GetTotal()
    {
        var totalAmount = DiscountItems.Sum(x =>
        {
            // Use DiscountedAmount if available; otherwise, default to Amount
            var itemTotal = x.Amount * x.Quantity;

            if (DiscountPercentage > 0)
            {
                var discountAmount = itemTotal * DiscountPercentage;
                itemTotal -= discountAmount;
            }

            if (x.Discount is > 0)
            {
                itemTotal -= x.Discount.Value;
            }

            // Apply tax
            if (x.Taxable && itemTotal > 0)
            {
                return itemTotal * (1 + TaxRate);
            }

            return itemTotal;
        });

        return totalAmount;
    }
}