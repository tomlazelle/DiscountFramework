using Discount.Tests.Configuration;
using Discount.Tests.FakeDomain;
using DiscountFramework;
using Shouldly;

namespace Discount.Tests.DiscountTests;

[Collection(nameof(DatabaseOnlyContainersCollection))]
public class BuyOneGetTwoFreeDifferentItemsTests : Subject<DiscountService>
{
    [Fact]
    public async Task can_add_two_items_and_get_one_for_free()
    {
        var cart = _fixture.CreateCart();

        cart.Items.Add(_fixture.CreateItem("1"));
        cart.Items.Add(_fixture.CreateItem("2"));
        cart.Items.Add(_fixture.CreateItem("3"));

        var discount = TestDiscountFactory.BOGOFreeDiscount(new[]
        {
            new Product
            {
                MustBuy = true, SKU = "1", Quantity = 1,
                
            },
            new Product
            {
                Free = true, SKU = "2", DiscountPercentage = 1, Quantity = 1
            },
            new Product
            {
                Free = true, SKU = "3", DiscountPercentage = 1, Quantity = 1
            }
        });

        await SaveDiscounts([discount]);

        var result = await Sut.ApplyDiscount(cart, "");

        result.Cart.DiscountItems.Count(x => x.SKU is "2" or "3" && x.Discount >= 0).ShouldBe(2);

        var cartTotalShouldBe = result.Cart.GetCartTotalWithTax();

        result.Cart.Total.ShouldBe(cartTotalShouldBe);
    }
}