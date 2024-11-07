using Discount.Tests.Configuration;
using Discount.Tests.FakeDomain;
using DiscountFramework;
using Shouldly;

namespace Discount.Tests.DiscountTests;

[Collection(nameof(DatabaseOnlyContainersCollection))]
public class BuyOneGetPercentOffWithCouponTests : Subject<DiscountService>
{
    [Fact]
    public async Task buy_one_percent_off_with_coupon_code()
    {
        var cart = _fixture.CreateCart();

        cart.Items.Add(_fixture.CreateItem("1"));
        cart.Items.Add(_fixture.CreateItem("2"));

        var discount = TestDiscountFactory.ProductDiscountWithCouponCode(new Product[]
        {
            new()
            {
                SKU = "1",
                DiscountPercentage = .5m,
                Quantity = 1,
                CreateDate = DateTime.Now,
                CreatedBy = "TEST",
                Name = "TEST",
                MustBuy = true
            }
        }, cart.CouponCode);

        await SaveDiscounts([discount]);

        var result = await Sut.ApplyDiscount(cart, cart.CouponCode);

        result.Cart.DiscountItems.First(x => x.SKU == "1").Discount.ShouldBe(2.5m);
    }
}