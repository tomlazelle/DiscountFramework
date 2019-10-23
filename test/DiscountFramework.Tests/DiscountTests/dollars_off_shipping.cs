using DiscountFramework.Tests.Configuration;
using DiscountFramework.Tests.FakeDomain;
using Shouldly;

namespace DiscountFramework.Tests.DiscountTests
{
    public class dollars_off_shipping:Subject<DiscountService>
    { 
        
        public void can_take_dollars_off()
        {
            var cart = _fixture.CreateCart();
            cart.ShippingAmount = 10;

            cart.AddItem(_fixture.CreateItem(1));
            cart.AddItem(_fixture.CreateItem(2));

            var discount = TestDiscountFactory.DollarsOffShippingDiscount(5);

            var result = Sut.ApplyDiscount(cart, discount);

            result.Cart.DiscountedShippingAmount.ShouldBe(5);
        }
    }
}