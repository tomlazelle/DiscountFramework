using System.Linq;
using DiscountFramework.Tests.FakeDomain;
using DiscountFramework.Tests.Fixtures;
using Shouldly;

namespace DiscountFramework.Tests.DiscountTests;

public class BuyOneGetOneFreeTests : DomainSubject<DiscountService>
{

    public void can_add_two_items_and_get_one_for_free()
    {
        var cart = _fixture.CreateCart();

        cart.Items.Add(_fixture.CreateItem("1"));
        cart.Items.Add(_fixture.CreateItem("2"));

        var discount = TestDiscountFactory.BOGOFreeDiscount(new[]
        {
            new Product {MustBuy = true, SKU = "1", Quantity = 1},
            new Product {Free = true, SKU = "2", DiscountPercentage = 1, Quantity = 1}
        });


        var result = Sut.ApplyDiscount(cart, discount);

        result.Cart.DiscountItems.First(x => x.SKU == "2").DiscountedAmount.ShouldBe(5);
        result.Cart.DiscountItems.First(x => x.SKU == "1").DiscountedAmount.ShouldBe(0);
    }
}