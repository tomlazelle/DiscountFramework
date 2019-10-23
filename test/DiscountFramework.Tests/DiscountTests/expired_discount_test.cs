using System;
using DiscountFramework.Tests.Configuration;
using DiscountFramework.Tests.FakeDomain;
using Shouldly;

namespace DiscountFramework.Tests.DiscountTests
{
    public class expired_discount_test:Subject<DiscountService>
    {
      

        public void can_not_use_an_expired_discount(){
            var cart = _fixture.CreateCart();

            cart.AddItem(_fixture.CreateItem(1));
            cart.AddItem(_fixture.CreateItem(2));

            var discount = TestDiscountFactory.ExpiredDiscount(DateTime.Now.AddDays(-60), DateTime.Now.AddDays(-30));

            
            var result = Sut.ApplyDiscount(cart, discount);

            result.Success.ShouldBeFalse();
            result.Error.ShouldBe("Invalid Discount");
        }
    }
}