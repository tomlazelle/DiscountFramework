using System.Linq;
using DiscountFramework.EnumTypes;

namespace DiscountFramework.Adjustments;

public class PercentageOffProduct : IAdjustment
{
    public DiscountCart Handle(DiscountCart cart, Discount discount)
    {
        if (discount.Type.Equals(DiscountType.AppliedToProducts) &&
            discount.UsePercentage)
        {

            var mustBuy = discount.DiscountProducts.Where(t => t.MustBuy).ToList();
            var free = discount.DiscountProducts.Where(t => t.Free).ToList();

            if (!mustBuy.Any() || !free.Any())
            {
                return cart;
            }
            

            // BOGO Buy one get one
            // buy item is the one you must buy
            foreach (var buyItem in mustBuy)
            {
                foreach (var freeItem in free)
                {
                    var disCounted = cart.DiscountItems.First(x => x.SKU == freeItem.SKU);
                    disCounted.DiscountedAmount = disCounted.Amount * freeItem.DiscountPercentage.Value;
                }

            }



        }

        return cart;
    }
}