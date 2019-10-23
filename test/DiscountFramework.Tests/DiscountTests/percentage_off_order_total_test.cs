using DiscountFramework.Tests.Configuration;
using DiscountFramework.Tests.FakeDomain;
using Shouldly;

namespace DiscountFramework.Tests.DiscountTests
{
    public class percentage_off_order_total_test : Subject<DiscountService>
    {

        public void can_reduce_cart_amount_by_seventy_five_percent_on_order_total()
        {
            var cart = _fixture.CreateCart();

            var discountAmt = .75m;
            var actualDiscount = cart.SubTotal * discountAmt;
            var discount = TestDiscountFactory.PercentageOffDiscount(discountAmt);

            var result = Sut.ApplyDiscount(cart, discount);

            result.Cart.DiscountedTotal.ShouldBe(cart.TotalPlusTaxMinusDiscount(actualDiscount));
        }

    }


}