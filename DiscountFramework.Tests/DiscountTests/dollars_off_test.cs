using AutoMapper;
using DiscountFramework.Configuration;
using DiscountFramework.TestObjects;
using DiscountFramework.Tests.Configuration;
using DiscountFramework.Tests.FakeDomain;
using Ploeh.AutoFixture;
using Should;

namespace DiscountFramework.Tests.DiscountTests
{
    public class dollars_off_test : BaseTest
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

        public void can_reduce_cart_amount_by_5_dollars_on_order_total()
        {
            var cart = _registry.CreateCart();
            cart.AddItem(_registry.CreateItem(1, 10));

            var discountAmt = 5m;

            var discount = TestDiscountFactory.DollarsOffDiscount(discountAmt);

            var discountService = new DiscountService();

            var result = discountService.ApplyDiscount(cart, discount);

            result.DiscountedTotal.ShouldEqual(cart.TotalPlusTaxMinusDiscount(5));
        }

    }


}