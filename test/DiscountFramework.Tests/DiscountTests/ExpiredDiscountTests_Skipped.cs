﻿using System;
using DiscountFramework.Tests.Configuration;
using DiscountFramework.Tests.FakeDomain;
using Shouldly;

namespace DiscountFramework.Tests.DiscountTests
{
    public class ExpiredDiscountTests_Skipped:Subject<DiscountService>
    {
      

        public void can_not_use_an_expired_discount(){
            // var cart = _fixture.CreateCart();
            //
            // cart.Items.Add(_fixture.CreateItem("1"));
            // cart.Items.Add(_fixture.CreateItem("2"));
            //
            // var discount = TestDiscountFactory.ExpiredDiscount(DateTime.Now.AddDays(-60), DateTime.Now.AddDays(-30));
            //
            //
            // var result = Sut.ApplyDiscount(cart, discount);
            //
            // result.Success.ShouldBeFalse();
            // result.Error.ShouldBe("Invalid DiscountPercentage");
        }
    }
}