using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DiscountFramework.Containers;

namespace DiscountFramework.Configuration
{
    public class MappingSetup : Profile
    {
        public MappingSetup()
        {
            CreateMap<Cart, DiscountCart>()
                .ForMember(dm => dm.TaxRate, mo => mo.MapFrom(sm => sm.TaxRate))
                .ForMember(dm => dm.DiscountItems, mo => mo.MapFrom(sm => sm.Items));


            CreateMap<CartItem, DiscountItem>()
                .ForMember(dm => dm.SKU, mo => mo.MapFrom(sm => sm.SKU))
                .ForMember(dm => dm.Amount, mo => mo.MapFrom(sm => sm.Amount))
                .ForMember(dm => dm.Quantity, mo => mo.MapFrom(sm => sm.Quantity))
                .ForMember(dm => dm.Taxable, mo => mo.MapFrom(sm => sm.Taxable))
                .ForMember(dm => dm.KitSKUList, mo => mo.MapFrom(sm => sm.KitSKUList));

            
        }
    }
}