using Discount.Tests.Configuration;
using Discount.Tests.FakeDomain;
using DiscountFramework;
using Shouldly;
using Xunit.Abstractions;


namespace Discount.Tests.DiscountTests;


[Collection(nameof(DatabaseOnlyContainersCollection))]
public class BuyOneGetOneFreeTests : Subject<DiscountService>
{
    private readonly ITestOutputHelper _testOutputHelper;

    public BuyOneGetOneFreeTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public async Task can_add_two_items_and_get_one_for_free()
    {
        var cart = _fixture.CreateCart();

        cart.Items.Add(_fixture.CreateItem("1"));
        cart.Items.Add(_fixture.CreateItem("2"));

        var discounts = TestDiscountFactory.BOGOFreeDiscount(new[]
        {
            new Product {MustBuy = true, SKU = "1", Quantity = 1},
            new Product {MustBuy = true, Free = true, SKU = "2", DiscountPercentage = 1, Quantity = 1}
        });

        discounts.TenantId = cart.TenantId;
        discounts.Enabled = true;

        await SaveDiscounts([discounts]);

        var result = await Sut.ApplyDiscount(cart, "");

        result.Cart.DiscountItems.First(x => x.Discount == 5).ShouldNotBeNull();
        result.Cart.DiscountItems.First(x => x.Discount == 0).ShouldNotBeNull();

        // print each item amount
        foreach (var item in result.Cart.DiscountItems)
        {
            _testOutputHelper.WriteLine(
                $"Item: {item.SKU} " +
                $"Amount: {item.Amount} " +
                $"Discount: {item.Discount}");
        }

        var cartTotalShouldBe = result.Cart.GetCartTotalWithTax();

        result.Cart.Total.ShouldBe(cartTotalShouldBe);
    }
}