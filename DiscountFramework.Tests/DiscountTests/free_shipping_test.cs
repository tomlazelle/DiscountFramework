using System.Linq;
using AutoMapper;
using DiscountFramework.Configuration;
using DiscountFramework.Tests.Configuration;
using DiscountFramework.Tests.FakeDomain;
using Ploeh.AutoFixture;
using Should;

namespace DiscountFramework.Tests.DiscountTests
{
    public class free_shipping_test : BaseTest
    {
        private IFixture _registry;

        public override void FixtureSetup(IFixture registry)
        {
            _registry = registry;
            Mapper.Initialize(x => x.AddProfile<MappingSetup>());
        }

        public override void FixtureTeardown()
        {

        }

        public void can_get_free_shipping()
        {
            var cart = _registry.CreateCart();

            cart.AddItem(_registry.CreateItem(1));
            cart.AddItem(_registry.CreateItem(2));

            var discount = TestDiscountFactory.FreeShippingDiscount();

            var discountService = new DiscountService();

            var result = discountService.ApplyDiscount(cart, discount);

            result.DiscountedShippingAmount.ShouldEqual(0);
        }
    }
}