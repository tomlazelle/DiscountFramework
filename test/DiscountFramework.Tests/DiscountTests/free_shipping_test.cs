
using DiscountFramework.Tests.Configuration;
using DiscountFramework.Tests.FakeDomain;
using Shouldly;

namespace DiscountFramework.Tests.DiscountTests
{
    public class free_shipping_test : Subject<DiscountService>
    {
        
        public void can_get_free_shipping()
        {
            var cart = _fixture.CreateCart();

            cart.AddItem(_fixture.CreateItem(1));
            cart.AddItem(_fixture.CreateItem(2));

            var discount = TestDiscountFactory.FreeShippingDiscount();

            var result = Sut.ApplyDiscount(cart, discount);

            result.Cart.DiscountedShippingAmount.ShouldBe(0);
        }
    }
}