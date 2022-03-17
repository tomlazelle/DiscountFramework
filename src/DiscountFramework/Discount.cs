using System;
using System.Collections.Generic;
using DiscountFramework.EnumTypes;

namespace DiscountFramework
{
    public class Discount
    {
        public string Id { get; set; }
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
        public bool Enabled { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreatedBy { get; set; }

        public List<Product> DiscountProducts { get; set; }
    }
}