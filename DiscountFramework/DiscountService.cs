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

        public DiscountCart ApplyDiscount(CartView cartView, Discount discount)
        {
            _discount = discount;

            _discountCart = Mapper.Map<CartView, DiscountCart>(cartView);


            if (DiscountType.AssignedToOrderTotal == _discount.Type)
            {
                AdjustOrderTotal();
            }

            if (DiscountType.AssignedToProducts == _discount.Type)
            {
                AdjustProducts();
            }


            return _discountCart;
        }

        private void AdjustProducts()
        {
            if (_discount.DiscountProducts.Any(x => x.Free))
            {
                AdjustBuyOneGetOne();
            }
        }

        private void AdjustOrderTotal()
        {
            if (!_discount.UsePercentage)
            {
                AdjustDollarsOff();
            }
        }

        private void AdjustBuyOneGetOne()
        {
            var discountIds = _discount.DiscountProducts.Select(x => x.ProductId).ToArray();
            var count = _discountCart.DiscountItems.Count(x => discountIds.Contains(x.ProductId));

            if (count == discountIds.Length)
            {

                foreach (var discoItem in _discount.DiscountProducts.Where(x=>!x.MustBuy))
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
                            item.Discount = item.Amount*discoItem.DiscountPercentage.Value;
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
                _discountCart.DiscountedAmount = _discountCart.OrignalTotal - _discount.DiscountAmount.Value;
            }
        }
    }
}