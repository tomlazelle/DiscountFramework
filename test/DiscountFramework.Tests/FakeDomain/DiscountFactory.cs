using System;
using System.Linq;
using DiscountFramework.EnumTypes;

namespace DiscountFramework.Tests.FakeDomain
{
    public static class TestDiscountFactory
    {
        // public static Discount FreeShippingDiscount()
        // {
        //     return new Discount
        //     {
        //         Type = DiscountType.AssignedToShipping,
        //         Limit = DiscountLimit.Unlimited,
        //         UsePercentage = true,
        //         DiscountPercentage = 1,
        //         StartDate = DateTime.Now,
        //         EndDate = DateTime.Now.AddDays(1)
        //     };
        // }
        // public static Discount DollarsOffShippingDiscount(decimal dollarsOff)
        // {
        //     return new Discount
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
        // public static Discount ExpiredDiscount(DateTime startDate,DateTime endDate)
        // {
        //     return new Discount
        //     {
        //         Type = DiscountType.AssignedToShipping,
        //         Limit = DiscountLimit.Unlimited,
        //         UsePercentage = false,
        //         StartDate = startDate,
        //         EndDate = endDate
        //     };
        // }


        // public static Discount DollarsOffItemDiscount(Product[] products)
        // {
        //     return new Discount
        //     {
        //         DiscountAmount = 0,
        //         Type = DiscountType.AssignedToProducts,
        //         Limit = DiscountLimit.Unlimited,
        //         DiscountProducts = products,
        //         StartDate = DateTime.Now,
        //         EndDate = DateTime.Now.AddDays(1)
        //     };
        // }

        public static Discount ProductDiscountWithCouponCode(Product[] products,  string couponCode)
        {
            return new Discount
            {
                Type = DiscountType.AppliedToProducts,
                Limit = DiscountLimit.NTimesOnly,
                NTimes = 1,
                DiscountProducts = products.ToList(),
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(1),
                UsePercentage = true,
                RequiresCouponCode = true,
                CouponCode = couponCode
            };
        }

        public static Discount BOGOFreeDiscount(Product[] products)
        {
            return new Discount
            {
                Type = DiscountType.AppliedToProducts,
                Limit = DiscountLimit.NTimesOnly,
                NTimes = 1,
                DiscountProducts = products.ToList(),
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(1),
                DiscountPercentage = 100m,
                UsePercentage = true
            };
        }

        public static Discount DollarsOffDiscountFromTotal(decimal dollarsOff)
        {
            return new Discount
            {
                DiscountAmount = dollarsOff,
                Type = DiscountType.AppliedToOrderTotal,
                Limit = DiscountLimit.Unlimited,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(1)
            };
        }

        public static Discount PercentageOffDiscountFromTotal(string name, decimal percentageOff)
        {
            return new Discount
            {
                Type = DiscountType.AppliedToOrderTotal,
                Limit = DiscountLimit.Unlimited,
                NTimes = 1,
                DiscountPercentage = percentageOff,
                UsePercentage = true,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(1),
                Enabled = true,
                Name = name,
                CouponCode = name
            };
        }
    }
}