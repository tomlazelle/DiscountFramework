using AutoMapper;
using DiscountFramework.Configuration;
using DiscountFramework.Tests.Configuration;
using DiscountFramework.Tests.FakeDomain;
using Ploeh.AutoFixture;
using Should;

namespace DiscountFramework.Tests.DiscountTests
{
    public class dollars_off_shipping:BaseTest
    {
        private IFixture _registry;

        public override void FixtureSetup(IFixture registry)
        {
            _registry = registry;
            Mapper.Initialize(x => x.AddProfile<MappingSetup>());
        }

        public override void FixtureTeardown()
        {
            var cart = _registry.CreateCart();
            cart.ShippingAmount = 10;

            cart.AddItem(_registry.CreateItem(1));
            cart.AddItem(_registry.CreateItem(2));

            var discount = TestDiscountFactory.DollarsOffShippingDiscount(5);

            var discountService = new DiscountService();

            var result = discountService.ApplyDiscount(cart, discount);

            result.Cart.DiscountedShippingAmount.ShouldEqual(5);
        }
    }
}