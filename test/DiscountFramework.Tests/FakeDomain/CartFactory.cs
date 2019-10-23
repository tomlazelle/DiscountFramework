using AutoFixture;
using DiscountFramework.TestObjects;

namespace DiscountFramework.Tests.FakeDomain
{
    public static class CartFactory
    {
        public static CartView CreateCart(this IFixture fixture)
        {
            return fixture.Build<CartView>()
                .Without(x => x.Items)
                .With(x => x.Tax, .0825m)
                .Create();
        }

        public static CartItemView CreateItem(this IFixture fixture, int productId, decimal amount = 5, int quantity = 1)
        {
            return fixture.Build<CartItemView>()
                .With(x => x.Amount, amount)
                .With(x => x.ProductId, productId)
                .With(x => x.Quantity, quantity)
                .Create();
        }

        public static decimal TotalPlusTaxMinusDiscount(this CartView cart,decimal discount)
        {
            var tax = (cart.SubTotal - discount) * cart.Tax;

            return (cart.SubTotal - discount) + tax ;
        }
    }
}