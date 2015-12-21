using System;
using AutoMapper;
using DiscountFramework.Configuration;
using DiscountFramework.Tests.Configuration;
using DiscountFramework.Tests.FakeDomain;
using Ploeh.AutoFixture;
using Should;

namespace DiscountFramework.Tests.DiscountTests
{
    public class expired_discount_test:BaseTest
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

        public void can_not_use_an_expired_discount(){
            var cart = _registry.CreateCart();

            cart.AddItem(_registry.CreateItem(1));
            cart.AddItem(_registry.CreateItem(2));

            var discount = TestDiscountFactory.ExpiredDiscount(DateTime.Now.AddDays(-60), DateTime.Now.AddDays(-30));

            var discountService = new DiscountService();

            var result = discountService.ApplyDiscount(cart, discount);

            result.Success.ShouldBeFalse();
            result.Error.ShouldEqual("Invalid Discount");
        }
    }
}