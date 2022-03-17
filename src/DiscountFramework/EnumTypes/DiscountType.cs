namespace DiscountFramework.EnumTypes
{
    public class DiscountType:Enumeration
    {

        public static DiscountType AppliedToOrderTotal = new DiscountType(1, "AssignedToOrderTotal");
        public static DiscountType AppliedToProducts = new DiscountType(2, "AssignedToProducts");
        public static DiscountType AppliedToShipping = new DiscountType(3, "AssignedToShipping");
        public static DiscountType AppliedToOrderSubTotal = new DiscountType(4, "AssignedToOrderSubTotal");        

        public DiscountType()
        {
        }

        public DiscountType(int value, string displayName) : base(value, displayName)
        {
        }
    }
}