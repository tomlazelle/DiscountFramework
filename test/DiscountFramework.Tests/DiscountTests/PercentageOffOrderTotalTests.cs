using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiscountFramework.Containers;
using DiscountFramework.Handlers;
using DiscountFramework.Tests.FakeDomain;
using DiscountFramework.Tests.Fixtures;
using Microsoft.Extensions.DependencyInjection;
using Raven.Client.Documents;
using Shouldly;

namespace DiscountFramework.Tests.DiscountTests
{
    public class PercentageOffOrderTotalTests : DomainSubject<DiscountHandler>
    {

        public async Task can_reduce_cart_amount_by_seventy_five_percent_on_order_total()
        {
            var discountName = Guid.NewGuid().ToString();
            var discountAmt = .75m;
            var discount = TestDiscountFactory.PercentageOffDiscountFromTotal(discountName, discountAmt);

            using (var session = _serviceProvider.GetService<IDocumentStore>().OpenAsyncSession())
            {
                await session.StoreAsync(discount);
                await session.SaveChangesAsync();
            }

            var cart = _fixture.CreateCart();
            cart.Items = new List<CartItem>
                         {
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
                         };

            var cartSubTotal = cart.Items.Sum(x => x.Quantity * x.Amount);
            var subTotalWithTax = cartSubTotal + (cartSubTotal * (1 * cart.TaxRate));

            //after tax discount
            var actualDiscount = subTotalWithTax * discountAmt;
            var actualDiscountedTotal = subTotalWithTax - actualDiscount;

            var response = await Sut.Execute(new DiscountRequest
            {
                DiscountCode = discountName,
                Cart = cart
            });


            var result = response.Data.Cart;

            result.SubTotal.ShouldBe(cartSubTotal);
            

            result.SubTotal.ShouldBe(actualDiscountedTotal);
        }

    }


}