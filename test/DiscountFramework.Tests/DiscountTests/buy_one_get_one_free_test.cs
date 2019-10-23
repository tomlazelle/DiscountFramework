using System.Linq;
using AutoMapper;
using DiscountFramework.Configuration;
using DiscountFramework.Tests.Configuration;
using DiscountFramework.Tests.FakeDomain;
using Shouldly;

namespace DiscountFramework.Tests.DiscountTests
{
    public class buy_one_get_one_free_test : Subject<DiscountService>
    {


        public void can_add_two_items_and_get_one_for_free()
        {
            var cart = _fixture.CreateCart();

            cart.AddItem(_fixture.CreateItem(1));
            cart.AddItem(_fixture.CreateItem(2));

            var discount = TestDiscountFactory.BOGOFreeDiscount(new[]
            {
                new DiscountProduct {MustBuy = true,ProductId = 1},
                new DiscountProduct {Free = true,ProductId = 2},
            });

            
            var result = Sut.ApplyDiscount(cart, discount);

            result.Cart.DiscountItems.First(x => x.ProductId == 2).DiscountedAmount.ShouldBe(0);
        }

    }
}