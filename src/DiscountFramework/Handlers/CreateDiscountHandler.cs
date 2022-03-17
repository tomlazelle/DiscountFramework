using System.Threading.Tasks;
using CommandQuery.Framing;

namespace DiscountFramework.Handlers
{
    public class CreateDiscountMessage : IMessage
    {
        public Discount Discount { get; set; }
    }

    public class CreateDiscountHandler : IAsyncHandler<CreateDiscountMessage, CommandResponse<Discount>>
    {
        public Task<CommandResponse<Discount>> Execute(CreateDiscountMessage message)
        {
            throw new System.NotImplementedException();
        }
    }
}