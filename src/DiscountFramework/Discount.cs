using System;
using System.Collections;
using System.Collections.Generic;
using DiscountFramework.EnumTypes;
using DiscountFramework.TestObjects;

namespace DiscountFramework
{
    public class Discount
    {
        public virtual string Name { get; set; }
        public virtual DiscountType Type { get; set; }
        public virtual bool UsePercentage { get; set; }
        public virtual decimal? DiscountPercentage { get; set; }
        public virtual decimal? DiscountAmount { get; set; }
        public virtual DateTime StartDate { get; set; }
        public virtual DateTime EndDate { get; set; }
        public virtual bool RequiresCouponCode { get; set; }
        public virtual string CouponCode { get; set; }
        public virtual DiscountLimit Limit { get; set; }
        public virtual int NTimes { get; set; }

        public IEnumerable<DiscountProduct> DiscountProducts { get; set; }
    }
}