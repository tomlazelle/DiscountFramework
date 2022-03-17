using System.Linq;
using DiscountFramework.Tests.Configuration;
using DiscountFramework.Tests.FakeDomain;
using Shouldly;

namespace DiscountFramework.Tests.DiscountTests
{
    public class BuyOneGetTwoFreeSameItemsTests_Skipped : Subject<DiscountService>
    {
       
        public void can_add_two_items_and_get_one_for_free()
        {
            // var cart = _fixture.CreateCart();
            //
            // cart.Items.Add(_fixture.CreateItem("1"));
            // cart.Items.Add(_fixture.CreateItem("2"));
            // cart.Items.Add(_fixture.CreateItem("2"));
            //
            // var discount = TestDiscountFactory.BOGOFreeDiscount(new[]
            // {
            //     new Product {MustBuy = true,ProductId = "1",SKU = "1"},
            //     new Product {Free = true,ProductId = "2",SKU = "2",Quantity = 2},
            // });
            //
            // var result = Sut.ApplyDiscount(cart, discount);
            //
            // result.Cart.DiscountItems.Count(x => x.SKU == "2" && x.DiscountedAmount == 0).ShouldBe(2);
        }

    }
}