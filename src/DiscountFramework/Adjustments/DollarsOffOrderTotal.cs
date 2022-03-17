using DiscountFramework.EnumTypes;

namespace DiscountFramework.Adjustments
{
    public class DollarsOffOrderTotal : IAdjustment
    {
        public DiscountCart Handle(DiscountCart cart, Discount discount)
        {
            if (!discount.UsePercentage && 
                discount.Type.Equals(DiscountType.AppliedToOrderTotal))
            {
                cart.Discount = discount.DiscountAmount.Value;
            }


            return cart;
        }
    }
}