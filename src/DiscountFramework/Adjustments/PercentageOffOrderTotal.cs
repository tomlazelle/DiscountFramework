using DiscountFramework.EnumTypes;

namespace DiscountFramework.Adjustments
{
    public class PercentageOffOrderTotal : IAdjustment
    {
        public DiscountCart Handle(DiscountCart cart, Discount discount)
        {
            if (discount.UsePercentage && 
                discount.Type.Equals(DiscountType.AppliedToOrderTotal))
            {
                var discounted = cart.TotalWithTaxAndDiscount * discount.DiscountPercentage.Value;
                cart.Discount = discounted;
            }

            return cart;
        }
    }
}