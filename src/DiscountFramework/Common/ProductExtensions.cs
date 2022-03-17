using System;

namespace DiscountFramework.Common
{
    public static class ProductExtensions
    {
        public static bool IsValidDate(this Discount value)
        {
            var today = DateTime.Now;

            return today >= value.StartDate && today <= value.EndDate;
        }

        public static bool IsEnabled(this Discount value)
        {
            return value.Enabled;
        }
    }
}