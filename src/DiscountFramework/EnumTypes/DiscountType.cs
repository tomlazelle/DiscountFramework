namespace DiscountFramework.EnumTypes
{
    public class DiscountType:Enumeration
    {

        public static DiscountType AssignedToOrderTotal = new DiscountType(1, "AssignedToOrderTotal");
        public static DiscountType AssignedToProducts = new DiscountType(2, "AssignedToProducts");
        public static DiscountType AssignedToShipping = new DiscountType(3, "AssignedToShipping");
        public static DiscountType AssignedToOrderSubTotal = new DiscountType(4, "AssignedToOrderSubTotal");        

        public DiscountType()
        {
        }

        public DiscountType(int value, string displayName) : base(value, displayName)
        {
        }
    }
}