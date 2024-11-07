using DiscountFramework;
using DiscountFramework.EnumTypes;


namespace Discount.Tests.FakeDomain
{
    public static class TestDiscountFactory
    {
        // public static DiscountPercentage FreeShippingDiscount()
        // {
        //     return new DiscountPercentage
        //     {
        //         Type = DiscountType.AssignedToShipping,
        //         Limit = DiscountLimit.Unlimited,
        //         UsePercentage = true,
        //         DiscountPercentage = 1,
        //         StartDate = DateTime.Now,
        //         EndDate = DateTime.Now.AddDays(1)
        //     };
        // }
        // public static DiscountPercentage DollarsOffShippingDiscount(decimal dollarsOff)
        // {
        //     return new DiscountPercentage
        //     {
        //         Type = DiscountType.AssignedToShipping,
        //         Limit = DiscountLimit.Unlimited,
        //         UsePercentage = false,
        //         DiscountAmount = dollarsOff,
        //         StartDate = DateTime.Now,
        //         EndDate = DateTime.Now.AddDays(1)
        //     };
        // }
        //
        // public static DiscountPercentage ExpiredDiscount(DateTime startDate,DateTime endDate)
        // {
        //     return new DiscountPercentage
        //     {
        //         Type = DiscountType.AssignedToShipping,
        //         Limit = DiscountLimit.Unlimited,
        //         UsePercentage = false,
        //         StartDate = startDate,
        //         EndDate = endDate
        //     };
        // }


        // public static DiscountPercentage DollarsOffItemDiscount(Product[] products)
        // {
        //     return new DiscountPercentage
        //     {
        //         DiscountAmount = 0,
        //         Type = DiscountType.AssignedToProducts,
        //         Limit = DiscountLimit.Unlimited,
        //         DiscountProducts = products,
        //         StartDate = DateTime.Now,
        //         EndDate = DateTime.Now.AddDays(1)
        //     };
        // }

        public static DiscountFramework.Discount ProductDiscountWithCouponCode(Product[] products,  string couponCode)
        {
            return new DiscountFramework.Discount
            {
                Type = DiscountType.AppliedToProducts,
                Limit = DiscountLimit.NTimesOnly,
                NTimes = 1,
                DiscountProducts = products.ToList(),
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(1),
                
                RequiresCouponCode = true,
                CouponCode = couponCode
            };
        }

        public static DiscountFramework.Discount BOGOFreeDiscount(Product[] products)
        {
            return new DiscountFramework.Discount
            {
                Type = DiscountType.AppliedToProducts,
                Limit = DiscountLimit.NTimesOnly,
                NTimes = 1,
                DiscountProducts = products.ToList(),
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(1),
                DiscountPercentage = 100m,
                
            };
        }

        public static DiscountFramework.Discount DollarsOffDiscountFromTotal(decimal dollarsOff)
        {
            return new DiscountFramework.Discount
            {
                DiscountAmount = dollarsOff,
                Type = DiscountType.AppliedToOrderTotal,
                Limit = DiscountLimit.Unlimited,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(1)
            };
        }

        public static DiscountFramework.Discount PercentageOffDiscountFromTotal(string name, decimal percentageOff)
        {
            return new DiscountFramework.Discount
            {
                Type = DiscountType.AppliedToOrderTotal,
                Limit = DiscountLimit.Unlimited,
                NTimes = 1,
                DiscountPercentage = percentageOff,
                
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(1),
                Enabled = true,
                Name = name,
                CouponCode = name,
                TenantId = Guid.NewGuid().ToString()
            };
        }
    }
}