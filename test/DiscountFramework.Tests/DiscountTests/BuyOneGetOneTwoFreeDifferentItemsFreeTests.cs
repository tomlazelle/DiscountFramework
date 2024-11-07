// using System.Linq;
// using DiscountFramework.Tests.FakeDomain;
// using DiscountFramework.Tests.Fixtures;
// using Shouldly;
//
// namespace DiscountFramework.Tests.DiscountTests
// {
//     public class BuyOneGetOneTwoFreeDifferentItemsFreeTests : DomainSubject<DiscountService>
//     {
//         public void can_add_two_items_and_get_one_for_free()
//         {
//             var cart = _fixture.CreateCart();
//             
//             cart.Items.Add(_fixture.CreateItem("1"));
//             cart.Items.Add(_fixture.CreateItem("2"));
//             cart.Items.Add(_fixture.CreateItem("3"));
//             
//             var discount = TestDiscountFactory.BOGOFreeDiscount(new[]
//             {
//                 new Product
//                 {
//                     MustBuy = true, SKU = "1",Quantity = 1
//                 },
//                 new Product
//                 {
//                     Free = true, SKU = "2",DiscountPercentage = 1,Quantity = 1
//                 },
//                 new Product
//                 {
//                     Free = true, SKU = "3",DiscountPercentage = 1,Quantity = 1
//                 },
//             });
//             
//             var result = Sut.ApplyDiscount(cart, discount);
//             
//             result.Cart.DiscountItems.Count(x => (x.SKU is "2" or "3") && x.DiscountedAmount >= 0).ShouldBe(2);
//         }
//     }
// }