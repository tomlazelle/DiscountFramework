using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DiscountFramework.EnumTypes;
using DiscountFramework.TestObjects;

namespace DiscountFramework
{
    public class DiscountService
    {
        private Discount _discount;
        private DiscountCart _discountCart;

        public DiscountResult ApplyDiscount(CartView cartView, Discount discount)
        {
            _discount = discount;

            if (!IsValidDiscount()) return new DiscountResult
            {
                Error = "Invalid Discount",
                Success = false
            };

            _discountCart = Mapper.Map<CartView, DiscountCart>(cartView);


            if (DiscountType.AssignedToOrderTotal == _discount.Type)
            {
                AdjustOrderTotal();
            }

            if (DiscountType.AssignedToProducts == _discount.Type)
            {
                AdjustProducts();
            }

            if (DiscountType.AssignedToShipping == _discount.Type)
            {
                AdjustShippingAmount();
            }

            return new DiscountResult
            {
                Cart = _discountCart,
                Success = true
            };
        }

        private bool IsValidDiscount()
        {
            if (_discount.StartDate > DateTime.Now) return false;

            if (_discount.EndDate < DateTime.Now) return false;

            return true;
        }

        private void AdjustShippingAmount()
        {
            if (_discount.UsePercentage)
            {
                var discount = _discountCart.OriginalShippingAmount*_discount.DiscountPercentage.Value;
                _discountCart.DiscountedShippingAmount = _discountCart.OriginalShippingAmount - discount;
            }

            if (!_discount.UsePercentage)
            {
                var discount = _discount.DiscountAmount.Value;
                _discountCart.DiscountedShippingAmount = _discountCart.OriginalShippingAmount - discount;
            }
        }

        private void AdjustProducts()
        {
            if (_discount.DiscountProducts.Any(x => x.Free))
            {
                AdjustBuyOneGetOne();
            }

            if (_discount.DiscountProducts.All(x => x.DiscountAmount != null))
            {
                AdjustProductTotal();
            }
        }

        private void AdjustProductTotal()
        {
            foreach (var discountItem in _discountCart.DiscountItems)
            {
                var foundItem = _discount.DiscountProducts.FirstOrDefault(x => x.ProductId == discountItem.ProductId);
                if (foundItem != null)
                {
                    discountItem.Discount = foundItem.DiscountAmount.Value;
                    discountItem.DiscountedAmount = discountItem.Amount - foundItem.DiscountAmount.Value;
                }
            }
        }

        private void AdjustOrderTotal()
        {
            if (!_discount.UsePercentage)
            {
                AdjustDollarsOff();
            }

            if (_discount.UsePercentage)
            {
                AdjustPercentageOff();
            }
        }

        private void AdjustPercentageOff()
        {
            if (_discount.Type == DiscountType.AssignedToOrderTotal)
            {
                var discount = _discountCart.OrignalTotal * _discount.DiscountPercentage.Value;
                _discountCart.Discount = discount;
            }
        }

        private void AdjustBuyOneGetOne()
        {
            var discountIds = new List<int>();

            //how many items do I need to get my discount
            foreach (var item in _discount.DiscountProducts.Where(x=>x.Free))
            {
                for (int i = 0; i < item.Quantity; i++)
                {
                    discountIds.Add(item.ProductId);
                }
            }

            var count = 0;

            foreach (var item in _discountCart.DiscountItems.Where(x => discountIds.Contains(x.ProductId)))
            {
                for (int i = 0; i < item.Quantity; i++)
                {
                    count++;
                }
            }

            if (count == discountIds.Count)
            {

                foreach (var discoItem in _discount.DiscountProducts.Where(x => !x.MustBuy))
                {

                    var items = _discountCart.DiscountItems.Where(x => x.ProductId == discoItem.ProductId);

                    if (discoItem.Free)
                    {
                        foreach (var item in items)
                        {
                            item.DiscountedAmount = 0;
                            item.Discount = item.Amount;
                        }

                    }

                    if (discoItem.DiscountAmount.HasValue)
                    {
                        foreach (var item in items)
                        {
                            item.Discount = discoItem.DiscountAmount.Value;
                            item.DiscountedAmount = item.Amount - discoItem.DiscountAmount.Value;
                        }
                    }

                    if (discoItem.DiscountPercentage.HasValue)
                    {
                        foreach (var item in items)
                        {
                            item.Discount = item.Amount * discoItem.DiscountPercentage.Value;
                            item.DiscountedAmount = item.Amount - item.Discount;
                        }
                    }
                }
            }

        }


        private void AdjustDollarsOff()
        {
            if (_discount.Type == DiscountType.AssignedToOrderTotal)
            {
                _discountCart.Discount = _discount.DiscountAmount.Value;
            }
        }
    }
}