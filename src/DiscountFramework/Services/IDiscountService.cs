using System.Threading.Tasks;
using DiscountFramework.Containers;

namespace DiscountFramework.Services
{
    public interface IDiscountService
    {
        Task<DiscountResult> ApplyDiscount(Cart cart, string couponCode);
    }
}