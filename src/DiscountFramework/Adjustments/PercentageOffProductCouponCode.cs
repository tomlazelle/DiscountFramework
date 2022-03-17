using System.Linq;
using DiscountFramework.EnumTypes;

namespace DiscountFramework.Adjustments;

public class PercentageOffProductCouponCode:IAdjustment
{
    public DiscountCart Handle(DiscountCart cart, Discount discount)
    {
        if (discount.Type.Equals(DiscountType.AppliedToProducts) &&
            discount.UsePercentage &&
            discount.RequiresCouponCode)
        {

            var mustBuy = discount.DiscountProducts.Where(t => t.MustBuy).ToList();

            if (!mustBuy.Any())
            {
                return cart;
            }

            


            foreach (var buyItem in mustBuy)
            {
                if (discount.CouponCode != cart.CouponCode)
                {
                    continue;
                }

                var disCounted = cart.DiscountItems.First(x => x.SKU == buyItem.SKU);
                disCounted.DiscountedAmount = disCounted.Amount * buyItem.DiscountPercentage.Value;
            }



        }

        return cart;
    }
}