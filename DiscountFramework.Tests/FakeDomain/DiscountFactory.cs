using System.Linq;
using DiscountFramework.EnumTypes;
using DiscountFramework.TestObjects;

namespace DiscountFramework.Tests.FakeDomain
{
    public static class TestDiscountFactory
    {
        public static Discount DollarsOffDiscount(decimal dollarsOff)
        {
            return new Discount
            {
                DiscountAmount = dollarsOff,
                Type = DiscountType.AssignedToOrderTotal,
                Limit = DiscountLimit.Unlimited
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
                DiscountPercentage = percentageOff
            };
        }
    }
}