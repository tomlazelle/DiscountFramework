﻿using System.Linq;
using AutoMapper;
using DiscountFramework.Configuration;
using DiscountFramework.TestObjects;
using DiscountFramework.Tests.Configuration;
using DiscountFramework.Tests.FakeDomain;
using Ploeh.AutoFixture;
using Should;

namespace DiscountFramework.Tests.DiscountTests
{
    public class buy_one_get_one_two_free_different_items_free_test : BaseTest
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

        public void can_add_two_items_and_get_one_for_free()
        {
            var cart = _registry.CreateCart();

            cart.AddItem(_registry.CreateItem(1));
            cart.AddItem(_registry.CreateItem(2));
            cart.AddItem(_registry.CreateItem(3));

            var discount = TestDiscountFactory.BOGOFreeDiscount(new[]
            {
                new DiscountProduct {MustBuy = true,ProductId = 1},
                new DiscountProduct {Free = true,ProductId = 2},
                new DiscountProduct {Free = true,ProductId = 3},
            });

            var discountService = new DiscountService();

            var result = discountService.ApplyDiscount(cart, discount);

            result.DiscountItems.Count(x => (x.ProductId == 2 || x.ProductId == 3) && x.DiscountedAmount == 0).ShouldEqual(2);
        }

    }
}