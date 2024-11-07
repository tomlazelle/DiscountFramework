using System;
using System.Collections.Generic;
using DiscountFramework.EnumTypes;

namespace DiscountFramework
{
    public class Discount
    {
        // primary id
        public string Id { get; set; }
        // tenant id
        public string TenantId { get; set; }
        // discount name
        public virtual string Name { get; set; }
        // discount Type
        // - AssignedToOrderTotal
        // - AssignedToProducts
        // - AssignedToShipping
        // - AssignedToOrderSubTotal
        public virtual DiscountType Type { get; set; }
        // discount will be a percentage off
        public virtual decimal? DiscountPercentage { get; set; }
        // discount will be a fixed amount off
        public virtual decimal? DiscountAmount { get; set; }
        // discount start date / time
        public virtual DateTime StartDate { get; set; }
        // discount end date / time
        public virtual DateTime EndDate { get; set; }
        // coupon code required
        public virtual bool RequiresCouponCode { get; set; }
        // actual consumer coupon code
        public virtual string CouponCode { get; set; }
        // the total number of times this discount can be used
        // - Unlimited
        // - NTimesOnly
        // - NTimesPerCustomer
        // - SingleUse
        public virtual DiscountLimit Limit { get; set; }
        // the number of times this discount has been used
        public virtual int NTimes { get; set; }
        // discount is enabled for use
        public bool Enabled { get; set; }
        // date / time discount was created
        public DateTime CreateDate { get; set; }
        // user who created the discount
        public string CreatedBy { get; set; }

        // list of products that this discount can be used for
        public List<Product> DiscountProducts { get; set; }
    }
}