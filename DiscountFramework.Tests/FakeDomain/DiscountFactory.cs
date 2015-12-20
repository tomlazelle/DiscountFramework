using DiscountFramework.EnumTypes;

namespace DiscountFramework.Tests.FakeDomain
{
    public static class TestDiscountFactory
    {
        public static Discount FreeShippingDiscount()
        {
            return new Discount
            {
                Type = DiscountType.AssignedToShipping,
                Limit = DiscountLimit.Unlimited,
                UsePercentage = true,
                DiscountPercentage = 1,
            };
        }

        public static Discount DollarsOffDiscount(decimal dollarsOff)
        {
            return new Discount
            {
                DiscountAmount = dollarsOff,
                Type = DiscountType.AssignedToOrderTotal,
                Limit = DiscountLimit.Unlimited,
                
            };
        }

        public static Discount DollarsOffItemDiscount(DiscountProduct[] products)
        {
            return new Discount
            {
                DiscountAmount = 0,
                Type = DiscountType.AssignedToProducts,
                Limit = DiscountLimit.Unlimited,
                DiscountProducts = products
            };
        }

        public static Discount BOGOFreeDiscount(DiscountProduct[] products)
        {
            return new Discount
            {
                Type = DiscountType.AssignedToProducts,
                Limit = DiscountLimit.NTimesOnly,
                NTimes = 1,
                DiscountProducts = products
            };
        }

        public static Discount PercentageOffDiscount(decimal percentageOff)
        {
            return new Discount
            {
                Type = DiscountType.AssignedToOrderTotal,
                Limit = DiscountLimit.Unlimited,
                NTimes = 1,
                DiscountPercentage = percentageOff,
                UsePercentage = true
            };
        }
    }
}