using AutoMapper;
using DiscountFramework.Configuration;
using DiscountFramework.Tests.Configuration;
using DiscountFramework.Tests.FakeDomain;
using Ploeh.AutoFixture;
using Should;

namespace DiscountFramework.Tests.DiscountTests
{
    public class percentage_off_order_total_test : BaseTest
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

        public void can_reduce_cart_amount_by_seventy_five_percent_on_order_total()
        {
            var cart = _registry.CreateCart();

            var discountAmt = .75m;
            var actualDiscount = cart.SubTotal*discountAmt;
            var discount = TestDiscountFactory.PercentageOffDiscount(discountAmt);

            var discountService = new DiscountService();

            var result = discountService.ApplyDiscount(cart, discount);

            result.Cart.DiscountedTotal.ShouldEqual(cart.TotalPlusTaxMinusDiscount(actualDiscount));
        }

    }

    
}