using DiscountFramework.TestObjects;
using Ploeh.AutoFixture;

namespace DiscountFramework.Tests.FakeDomain
{
    public static class CartFactory
    {
        public static CartView CreateCart(this IFixture fixture)
        {
            return fixture.Build<CartView>()
                .Without(x => x.Items)
                .Create();
        }

        public static CartItemView CreateItem(this IFixture fixture,int productId,decimal amount = 5, int quantity = 1)
        {
            return fixture.Build<CartItemView>()
                .With(x => x.Amount, amount)
                .With(x => x.ProductId, productId)
                .With(x => x.Quantity, quantity)
                .Create();
        }

    }
}