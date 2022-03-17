using System;
using System.Collections.Generic;
using System.Linq;
using CommandQuery.Framing;
using DiscountFramework.Containers;
using System.Threading.Tasks;
using DiscountFramework.Common;
using DiscountFramework.Services;
using Raven.Client.Documents;
using Raven.Client.Documents.Linq;

namespace DiscountFramework.Handlers
{
    public class DiscountRequest : IMessage
    {
        public Cart Cart { get; set; }
        public string CouponCode { get; set; }
    }

    public class DiscountHandler : IAsyncHandler<DiscountRequest, CommandResponse<DiscountResult>>
    {
        private readonly IDiscountService _discountService;
        private readonly IDocumentStore _documentStore;

        public DiscountHandler(
            IDiscountService discountService,
            IDocumentStore documentStore)
        {
            _discountService = discountService;
            _documentStore = documentStore;
        }

        public async Task<CommandResponse<DiscountResult>> Execute(DiscountRequest message)
        {
            DiscountResult discountResult = default;

            if (!string.IsNullOrEmpty(message.CouponCode))
            {
                using (var session = _documentStore.OpenAsyncSession())
                {
                    var productDiscount = await session.Query<Discount>()
                                              .Customize(x => x.WaitForNonStaleResults(TimeSpan.FromSeconds(5)))
                                              .FirstOrDefaultAsync(x => x.CouponCode == message.CouponCode);

                    if (!productDiscount.IsValidDate())
                    {
                        var errorValidDate = $"Valid from {productDiscount.StartDate:MM/dd/yyyy} To {productDiscount.EndDate:MM/dd/yyyy}";
                        return Response
                            .Failed<DiscountResult>(new List<string>{
                                                                        $"Discount Code {message.CouponCode} is expired",
                                                                        errorValidDate});
                    }

                    if (!productDiscount.Enabled)
                    {
                        return Response
                            .Failed<DiscountResult>(new List<string> { $"Discount not enabled" });
                    }


                    discountResult = _discountService.Adjust(message.Cart, productDiscount);
                }
            }
            else
            {
                using (var session = _documentStore.OpenAsyncSession())
                {
                    var today = DateTime.Now;
                    var discountProductList = await session.Query<Discount>()
                                                  .Where(x => x.Enabled && x.StartDate >= today && x.EndDate <= today, true)
                                                  .ToListAsync();

                    if (!discountProductList.Any())
                    {
                        return Response
                            .Failed<DiscountResult>(new List<string> { $"No discounts found" });
                    }

                    discountResult = _discountService.Adjust(message.Cart, discountProductList);
                }

            }

            return Response.Ok(discountResult); ;
        }
    }
}