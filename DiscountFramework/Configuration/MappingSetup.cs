using AutoMapper;
using DiscountFramework.TestObjects;

namespace DiscountFramework.Configuration
{
    public class MappingSetup:Profile
    {
        protected override void Configure()
        {
            CreateMap<CartView, DiscountCart>()
                .ForMember(dm=>dm.CartId,mo=>mo.MapFrom(sm=>sm.Id))
                .ForMember(dm=>dm.DiscountItems,mo=>mo.MapFrom(sm=>sm.Items))
                .ForMember(dm=>dm.OriginalShippingAmount,mo=>mo.MapFrom(sm=>sm.ShippingAmount))
                ;

            CreateMap<CartItemView, DiscountItem>()
                .ForMember(dm=>dm.CartItemId,mo=>mo.MapFrom(sm=>sm.Id))
                ;
        }
    }
}