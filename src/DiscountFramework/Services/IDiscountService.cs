using System.Collections.Generic;
using DiscountFramework.Containers;

namespace DiscountFramework.Services
{
    public interface IDiscountService
    {
        DiscountResult Adjust(Cart messageCart, Discount discount);
        DiscountResult Adjust(Cart messageCart, List<Discount> discountList);
    }
}