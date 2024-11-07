using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiscountFramework.EnumTypes;

namespace DiscountFramework;

public interface IDiscountVisitor
{
    Task Visit(DiscountCart cart, List<Discount> productDiscounts);
}

public class DiscountVisitor : IDiscountVisitor
{
    public async Task Visit(DiscountCart cart, List<Discount> productDiscounts)
    {
        if (!string.IsNullOrEmpty(cart.CouponCode))
        {
            foreach (var discount in productDiscounts
                         .Where(x => x.RequiresCouponCode
                                     && x.CouponCode.Equals(cart.CouponCode)
                                     && x.Type.Equals(DiscountType.AppliedToProducts)))
            {
                foreach (var discountProduct in discount.DiscountProducts)
                {
                    // loop through cart discount items and match sku for discount percentage
                    foreach (var item in cart.DiscountItems)
                    {
                        if (item.SKU == discountProduct.SKU)
                        {
                            item.Discount = item.Amount * discountProduct.DiscountPercentage.Value;
                        }
                    }
                }
            }

            // Apply order total discounts
            foreach (var discount in productDiscounts
                         .Where(x => x.Type.Equals(DiscountType.AppliedToOrderTotal)))
            {
                if (discount.DiscountPercentage.HasValue)
                {
                    cart.DiscountPercentage = discount.DiscountPercentage.Value;
                }
                else if (discount.DiscountAmount.HasValue)
                {
                    cart.DiscountDollars = discount.DiscountAmount.Value;
                }
            }

            // Apply item discounts
            foreach (var discount in productDiscounts
                         .Where(x =>
                             x.Type.Equals(DiscountType.AppliedToProducts)
                             && !x.RequiresCouponCode))
            {
                foreach (var product in discount.DiscountProducts)
                {
                    if (product.MustBuy)
                    {
                        // Check if enough MustBuy items are in the cart
                        var mustBuyItems = cart.DiscountItems
                            .Where(i => i.SKU == product.SKU && i.Quantity >= product.Quantity)
                            .ToList();

                        if (mustBuyItems.Any())
                        {
                            // Apply Free product(s) to the cart
                            var freeProducts = discount.DiscountProducts.Where(p => p.Free).ToList();

                            foreach (var freeProduct in freeProducts)
                            {
                                var item = cart.DiscountItems.FirstOrDefault(x => x.SKU == freeProduct.SKU);
                                item.Discount = item.Amount;
                            }
                        }
                    }
                }
            }
        }
    }
}