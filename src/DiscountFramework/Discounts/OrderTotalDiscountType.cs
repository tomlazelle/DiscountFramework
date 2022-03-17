using CommandQuery.Framing;
using System.Threading.Tasks;
using DiscountFramework.EnumTypes;

namespace DiscountFramework.Discounts
{
    public class OrderTotalDiscountType : IAsyncHandler<OrderTotalTypeMessage, CommandResponse<DiscountResult>>
    {
        public Task<CommandResponse<DiscountResult>> Execute(OrderTotalTypeMessage message)
        {
            throw new System.NotImplementedException();
        }
    }
    public class ProductDiscountType : IAsyncHandler<ProductTypeMessage, CommandResponse<DiscountResult>>
    {
        public Task<CommandResponse<DiscountResult>> Execute(ProductTypeMessage message)
        {
            throw new System.NotImplementedException();
        }
    }
    public class ShippingDiscountType : IAsyncHandler<ShippingTypeMessage, CommandResponse<DiscountResult>>
    {
        public Task<CommandResponse<DiscountResult>> Execute(ShippingTypeMessage message)
        {
            throw new System.NotImplementedException();
        }
    }
    public abstract class DiscountTypeMessage:IMessage
    {
        public DiscountType Type { get; set; }
        public DiscountCart Cart { get; set; }
    }
    public class OrderTotalTypeMessage:DiscountTypeMessage
    {
        
    }
    public class ProductTypeMessage:DiscountTypeMessage
    {
        
    }
    public class ShippingTypeMessage:DiscountTypeMessage
    {
        
    }
    public static class DiscountFactory
    {
        public static DiscountResult Handle(DiscountCart discountCart)
        {
            return default;
        }
    }
}