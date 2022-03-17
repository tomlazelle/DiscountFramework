using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DiscountFramework.Adjustments;
using DiscountFramework.Containers;
using DiscountFramework.Services;

namespace DiscountFramework
{
    public class DiscountService:IDiscountService
    {
        private readonly List<IAdjustment> _adjustments;
        private readonly IMapper _mapper;

        public DiscountService(
            IEnumerable<IAdjustment> adjustments,
            IMapper mapper)
        {
            _adjustments = adjustments.ToList();
            _mapper = mapper;
        }

        public DiscountResult ApplyDiscount(Cart cart, Discount discount)
        {
            var discountCart = _mapper.Map<DiscountCart>(cart);

            foreach (var adjustment in _adjustments)
            {
                discountCart = adjustment.Handle(discountCart,
                                                 discount
                                                );
            }
            // if (DiscountType.AssignedToOrderTotal.Equals(_discount.Type))
            // {
            //     AdjustOrderTotal();
            // }

            // if (DiscountType.AssignedToProducts.Equals(_discount.Type))
            // {
            //     AdjustProducts();
            // }
            //
            // if (DiscountType.AssignedToShipping.Equals(_discount.Type))
            // {
            //     AdjustShippingAmount();
            // }

            return new DiscountResult
            {
                Cart = discountCart,
                Success = true
            };
        }

        // private void AdjustShippingAmount()
        // {
        //     if (_discount.UsePercentage)
        //     {
        //         var discount = _discountCart.OriginalShippingAmount*_discount.DiscountPercentage.Value;
        //         _discountCart.DiscountedShippingAmount = _discountCart.OriginalShippingAmount - discount;
        //     }
        //
        //     if (!_discount.UsePercentage)
        //     {
        //         var discount = _discount.DiscountAmount.Value;
        //         _discountCart.DiscountedShippingAmount = _discountCart.OriginalShippingAmount - discount;
        //     }
        // }

        // private void AdjustProducts()
        // {
        //     if (_discount.DiscountProducts.Any(x => x.Free))
        //     {
        //         AdjustBuyOneGetOne();
        //     }
        //
        //     if (_discount.DiscountProducts.All(x => x.DiscountAmount != null))
        //     {
        //         AdjustProductTotal();
        //     }
        // }

        // private void AdjustProductTotal()
        // {
        //     foreach (var discountItem in _discountCart.DiscountItems)
        //     {
        //         var foundItem = _discount.DiscountProducts.FirstOrDefault(x => x.SKU == discountItem.SKU);
        //         if (foundItem != null)
        //         {
        //             discountItem.Discount = foundItem.DiscountAmount.Value;
        //             discountItem.DiscountedAmount = discountItem.Amount - foundItem.DiscountAmount.Value;
        //         }
        //     }
        // }

        // private void AdjustOrderTotal()
        // {
        //     if (!_discount.UsePercentage)
        //     {
        //         AdjustDollarsOff();
        //     }
        //
        //     if (_discount.UsePercentage)
        //     {
        //         AdjustPercentageOff();
        //     }
        // }

        // moved to class PercentageOffOrderTotal
        // private void AdjustPercentageOff()
        // {
        //     if (_discount.Type.Equals(DiscountType.AssignedToOrderTotal))
        //     {
        //         var discount = _discountCart.OrignalTotal * _discount.DiscountPercentage.Value;
        //         _discountCart.Discount = discount;
        //     }
        // }

        // private void AdjustBuyOneGetOne()
        // {
        //     var skus = new List<string>();
        //
        //     //how many items do I need to get my discount
        //     foreach (var item in _discount.DiscountProducts.Where(x=>x.Free))
        //     {
        //         for (var i = 0; i < item.Quantity; i++)
        //         {
        //             skus.Add(item.SKU);
        //         }
        //     }
        //
        //     var count = 0;
        //
        //     foreach (var item in _discountCart.DiscountItems.Where(x => skus.Contains(x.SKU)))
        //     {
        //         for (var i = 0; i < item.Quantity; i++)
        //         {
        //             count++;
        //         }
        //     }
        //
        //     if (count == skus.Count)
        //     {
        //
        //         foreach (var discoItem in _discount.DiscountProducts.Where(x => !x.MustBuy))
        //         {
        //
        //             var items = _discountCart.DiscountItems.Where(x => x.SKU == discoItem.SKU).ToList();
        //
        //             if (discoItem.Free)
        //             {
        //                 foreach (var item in items)
        //                 {
        //                     item.DiscountedAmount = 0;
        //                     item.Discount = item.Amount;
        //                 }
        //
        //             }
        //
        //             if (discoItem.DiscountAmount.HasValue)
        //             {
        //                 foreach (var item in items)
        //                 {
        //                     item.Discount = discoItem.DiscountAmount.Value;
        //                     item.DiscountedAmount = item.Amount - discoItem.DiscountAmount.Value;
        //                 }
        //             }
        //
        //             if (discoItem.DiscountPercentage.HasValue)
        //             {
        //                 foreach (var item in items)
        //                 {
        //                     item.Discount = item.Amount * discoItem.DiscountPercentage.Value;
        //                     item.DiscountedAmount = item.Amount - item.Discount;
        //                 }
        //             }
        //         }
        //     }
        //
        // }

        //Moved to DollarsOffOrderTotal class
        // private void AdjustDollarsOff()
        // {
        //     if (_discount.Type.Equals(DiscountType.AssignedToOrderTotal))
        //     {
        //         _discountCart.Discount = _discount.DiscountAmount.Value;
        //     }
        // }

        public DiscountResult Adjust(Cart messageCart, Discount productDiscount)
        {
            return ApplyDiscount(messageCart,productDiscount);
        }

        public DiscountResult Adjust(Cart messageCart, List<Discount> discountProductList)
        {
            return default;
        }
    }
}