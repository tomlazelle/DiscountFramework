using System;
using Microsoft.Extensions.DependencyInjection;

namespace DiscountFramework.Common
{
    public static class ServiceProfileExtensions
    {
        public static IServiceCollection AddProfile<T>(this IServiceCollection serviceCollection) where T : ServiceProfile
        {
            return Activator.CreateInstance<T>().Register(serviceCollection);
        }
    }
}