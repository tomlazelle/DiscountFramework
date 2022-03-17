
using DiscountFramework.Tests.Configuration;
using DiscountFramework.Tests.FakeDomain;
using Shouldly;

namespace DiscountFramework.Tests.DiscountTests
{
    public class FreeShippingTests_Skipped : Subject<DiscountService>
    {
        
        public void can_get_free_shipping()
        {
            // var cart = _fixture.CreateCart();
            //
            // cart.Items.Add(_fixture.CreateItem("1"));
            // cart.Items.Add(_fixture.CreateItem("2"));
            //
            // var discount = TestDiscountFactory.FreeShippingDiscount();
            //
            // var result = Sut.ApplyDiscount(cart, discount);
            //
            // result.Cart.DiscountedShippingAmount.ShouldBe(0);
        }
    }
}