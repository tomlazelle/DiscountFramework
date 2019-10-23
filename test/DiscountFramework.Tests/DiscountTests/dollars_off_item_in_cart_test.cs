using System.Collections.Generic;
using System.Linq;
using DiscountFramework.Tests.Configuration;
using DiscountFramework.Tests.FakeDomain;
using Shouldly;

namespace DiscountFramework.Tests.DiscountTests
{
    public class dollars_off_item_in_cart_test : Subject<DiscountService>
    {
        public void item_in_cart_receives_discount()
        {
            var cart = _fixture.CreateCart();

            var discountItems = new List<DiscountProduct>();

            discountItems.Add(new DiscountProduct
            {
                ProductId = 1,
                DiscountAmount = 5
            });

            var discount = TestDiscountFactory.DollarsOffItemDiscount(discountItems.ToArray());

            cart.AddItem(_fixture.CreateItem(1, 10));


            var result = Sut.ApplyDiscount(cart, discount);

            result.Cart.DiscountedTotal.ShouldBe(5 + (5 * cart.Tax));
            result.Cart.DiscountItems.First().DiscountedAmount.ShouldBe(5);
        }
    }
}