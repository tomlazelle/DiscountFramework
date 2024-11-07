using System.Linq;
using DiscountFramework.Tests.Configuration;
using Shouldly;

namespace DiscountFramework.Tests.DiscountCartTests
{
    public class CartTests:Subject<DiscountCart>
    {
        public void subtotal_is_valid_for_multiple_items()
        {

            Sut.TaxRate = .0825m;
            Sut.DiscountItems = new[]
                                {
                                    new DiscountItem
                                    {
                                        Quantity = 1,
                                        Amount = 10,
                                        Taxable = true,
                                    },
                                    new DiscountItem
                                    {
                                        Quantity = 1,
                                        Amount = 20,
                                        Taxable = true,
                                    }
                                };

            var subtotal = Sut.DiscountItems.Sum(x => x.Amount * x.Quantity);
            var subtotalWithTax = subtotal * (1 + Sut.TaxRate);

            Sut.SubTotal.ShouldBe(subtotal);
            
        }

        public void total_is_valid_for_multiple_items_discount()
        {
            
            Sut.TaxRate = .0825m;
            Sut.DiscountItems = new[]
                                {
                                    new DiscountItem
                                    {
                                        Quantity = 1,
                                        Amount = 10,
                                        Taxable = true,
                                    },
                                    new DiscountItem
                                    {
                                        Quantity = 1,
                                        Amount = 20,
                                        Taxable = true,
                                    }
                                };

            // 30
            var subtotal = Sut.DiscountItems.Sum(x => x.Amount * x.Quantity);
            // 30.1650
            var subtotalWithTax = subtotal * (1 + Sut.TaxRate);
            // 22.50
            var discount = subtotal * .75m;
            Sut.DiscountPercentage = discount;

            // 7.6650
            var totalMinusDiscount = (subtotalWithTax - discount);

            Sut.SubTotal.ShouldBe(subtotal);
            
            Sut.SubTotal.ShouldBe(totalMinusDiscount);
        }

     
    }
}