namespace DiscountFramework.EnumTypes
{
    public class DiscountLimit:Enumeration
    {
        public static DiscountLimit Unlimited = new DiscountLimit(1,"Unlimited");
        public static DiscountLimit NTimesOnly = new DiscountLimit(2, "NTimesOnly");
        public static DiscountLimit NTimesPerCustomer = new DiscountLimit(3, "NTimesPerCustomer");

        public DiscountLimit()
        {
        }

        public DiscountLimit(int value, string displayName) : base(value, displayName)
        {
        }
    }
}