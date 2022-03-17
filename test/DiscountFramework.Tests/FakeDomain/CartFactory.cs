using AutoFixture;
using DiscountFramework.Containers;

namespace DiscountFramework.Tests.FakeDomain
{
    public static class CartFactory
    {
        public static Cart CreateCart(this IFixture fixture)
        {
            return fixture.Build<Cart>()
                .Without(x => x.Items)
                .With(x => x.TaxRate, .0825m)
                .Create();
        }

        public static CartItem CreateItem(this IFixture fixture, string sku, decimal amount = 5, int quantity = 1)
        {
            return fixture.Build<CartItem>()
                .With(x => x.Amount, amount)
                .With(x => x.SKU, sku)
                .With(x => x.Quantity, quantity)
                .Create();
        }
    }
}