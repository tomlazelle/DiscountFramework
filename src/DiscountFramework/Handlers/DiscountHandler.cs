using System.Threading.Tasks;
using CommandQuery.Framing;
using DiscountFramework.Containers;
using DiscountFramework.Services;

namespace DiscountFramework.Handlers;

public class DiscountRequest : IMessage
{
    public Cart Cart { get; set; }
    public string DiscountCode { get; set; }
}

public class DiscountHandler : IAsyncHandler<DiscountRequest, CommandResponse<DiscountResult>>
{
    private readonly IDiscountService _discountService;

    public DiscountHandler(
        IDiscountService discountService)
    {
        _discountService = discountService;
    }

    public async Task<CommandResponse<DiscountResult>> Execute(DiscountRequest message)
    {
        var discountResult = await _discountService
            .ApplyDiscount(message.Cart, message.DiscountCode);


        return Response.Ok(discountResult);
    }
}