using Discount.Tests.Configuration;
using Discount.Tests.FakeDomain;
using DiscountFramework.Containers;
using DiscountFramework.Handlers;
using Microsoft.Extensions.DependencyInjection;
using Raven.Client.Documents;
using Shouldly;

namespace Discount.Tests.DiscountTests;

[Collection(nameof(DatabaseOnlyContainersCollection))]
public class PercentageOffOrderTotalTests : Subject<DiscountHandler>
{
    [Fact]
    public async Task can_reduce_cart_amount_by_seventy_five_percent_on_order_total()
    {
        var discountName = Guid.NewGuid().ToString();
        var discountAmt = .75m;
        var discount = TestDiscountFactory.PercentageOffDiscountFromTotal(discountName, discountAmt);
        var documentStore = _serviceProvider.GetRequiredService<IDocumentStore>();

        using (var session = documentStore.OpenAsyncSession())
        {
            await session.StoreAsync(discount);
            await session.SaveChangesAsync();
        }


        var cart = _fixture.CreateCart();
        cart.TenantId = discount.TenantId;
        cart.Items =
        [
            new CartItem
            {
                SKU = "1",
                Quantity = 1,
                Amount = 10,
                Taxable = true
            },

            new CartItem
            {
                SKU = "2",
                Quantity = 1,
                Amount = 20,
                Taxable = true
            }
        ];

        var cartSubTotal = cart.Items.Sum(x => x.Quantity * x.Amount);
        var subTotalWithDiscount = cartSubTotal - cartSubTotal * discountAmt;
        var totalWithTax = subTotalWithDiscount * (1 + cart.TaxRate);

        var response = await Sut.Execute(new DiscountRequest
        {
            DiscountCode = discountName,
            Cart = cart
        });


        var result = response.Data.Cart;

        result.SubTotal.ShouldBe(subTotalWithDiscount);
        result.Total.ShouldBe(totalWithTax);
    }
}