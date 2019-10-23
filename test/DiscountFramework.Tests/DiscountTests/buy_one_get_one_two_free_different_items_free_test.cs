using System.Linq;
using DiscountFramework.Tests.Configuration;
using DiscountFramework.Tests.FakeDomain;
using Shouldly;

namespace DiscountFramework.Tests.DiscountTests
{
    public class buy_one_get_one_two_free_different_items_free_test : Subject<DiscountService>
    {
        public void can_add_two_items_and_get_one_for_free()
        {
            var cart = _fixture.CreateCart();

            cart.AddItem(_fixture.CreateItem(1));
            cart.AddItem(_fixture.CreateItem(2));
            cart.AddItem(_fixture.CreateItem(3));

            var discount = TestDiscountFactory.BOGOFreeDiscount(new[]
            {
                new DiscountProduct
                {
                    MustBuy = true, ProductId = 1
                },
                new DiscountProduct
                {
                    Free = true, ProductId = 2
                },
                new DiscountProduct
                {
                    Free = true, ProductId = 3
                },
            });

            var result = Sut.ApplyDiscount(cart, discount);

            result.Cart.DiscountItems.Count(x => (x.ProductId == 2 || x.ProductId == 3) && x.DiscountedAmount == 0).ShouldBe(2);
        }
    }
}