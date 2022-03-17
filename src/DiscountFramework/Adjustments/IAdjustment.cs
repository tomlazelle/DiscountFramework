namespace DiscountFramework.Adjustments
{
    public interface IAdjustment
    {
        DiscountCart Handle(DiscountCart cart, Discount discount);
    }
}