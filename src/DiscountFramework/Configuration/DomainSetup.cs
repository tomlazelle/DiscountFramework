using AutoMapper;
using CommandQuery.Framing;
using DiscountFramework.Common;
using DiscountFramework.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DiscountFramework.Configuration
{
    public class DomainSetup : ServiceProfile
    {
        public DomainSetup()
        {
            Service.AddSingleton(new MapperConfiguration(x => x.AddProfile<MappingSetup>()).CreateMapper());

            Service.AddCommandQuery(typeof(DomainSetup).Assembly);

            Service.AddTransient<IDiscountService, DiscountService>();

            Service.AddSingleton<IDiscountVisitor, DiscountVisitor>();

           
        }
    }
}