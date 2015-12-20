using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DiscountFramework.Configuration;
using DiscountFramework.Tests.Configuration;
using DiscountFramework.Tests.FakeDomain;
using Ploeh.AutoFixture;
using Should;

namespace DiscountFramework.Tests.DiscountTests
{
    public class dollars_off_item_in_cart_test : BaseTest
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

        public void item_in_cart_receives_discount()
        {
            var cart = _registry.CreateCart();

            var discountItems = new List<DiscountProduct>();

            discountItems.Add(new DiscountProduct
            {
                ProductId = 1,
                DiscountAmount = 5
            });

            var discount = TestDiscountFactory.DollarsOffItemDiscount(discountItems.ToArray());

            cart.AddItem(_registry.CreateItem(1, 10));

            var service = new DiscountService();
            var result = service.ApplyDiscount(cart, discount);

            result.DiscountedTotal.ShouldEqual(5 + (5 * cart.Tax));
            result.DiscountItems.First().DiscountedAmount.ShouldEqual(5);
        }
    }
}