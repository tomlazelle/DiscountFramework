namespace DiscountFramework.EnumTypes
{
    public class DiscountLimit:Enumeration
    {
        public static DiscountLimit Unlimited = new DiscountLimit(1,"Unlimited");
        public static DiscountLimit NTimesOnly = new DiscountLimit(2, "NTimesOnly");
        public static DiscountLimit NTimesPerCustomer = new DiscountLimit(3, "NTimesPerCustomer");
        public static DiscountLimit SingleUse = new DiscountLimit(4, "SingleUse");
        public static DiscountLimit BuyOneGetOne = new DiscountLimit(5, "BuyOneGetOne");
        public static DiscountLimit BuyXGetY = new DiscountLimit(6, "BuyXGetY");

        public DiscountLimit()
        {
        }

        public DiscountLimit(int value, string displayName) : base(value, displayName)
        {
        }
    }
}