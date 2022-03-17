using System;
using System.Linq;
using System.Threading.Tasks;
using DiscountFramework.Tests.FakeDomain;
using DiscountFramework.Tests.Fixtures;
using Shouldly;

namespace DiscountFramework.Tests.DiscountTests;

public class BuyOnePercentOffCouponCodeTests: DomainSubject<DiscountService>
{
    public void buy_one_percent_off_with_coupon_code()
    {
        var cart = _fixture.CreateCart();

        cart.Items.Add(_fixture.CreateItem("1"));
        cart.Items.Add(_fixture.CreateItem("2"));

        var discount = TestDiscountFactory.ProductDiscountWithCouponCode(new Product[]
        {
            new Product
            {
                SKU = "1",
                DiscountPercentage = .5m,
                Quantity = 1,
                CreateDate = DateTime.Now,
                CreatedBy = "TEST",
                Name = "TEST",
                MustBuy = true
            }

        },cart.CouponCode);


        var result = Sut.ApplyDiscount(cart, discount);

        result.Cart.DiscountItems.First(x => x.SKU == "1").DiscountedAmount.ShouldBe(2.5m);
    }
}