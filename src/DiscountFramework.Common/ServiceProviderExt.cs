using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace DiscountFramework.Common
{
    public static class ServiceProviderExt
    {
        public static IServiceCollection AddTransientByName<TService, TImplementation>(
            this IServiceCollection serviceCollection,
            string key)
            where TService : class where TImplementation : class
        {
            var sw = new ServiceWrap<TService>
            {
                Key = key,
                Type = sp => sp.GetService<TImplementation>() as TService
            };
            serviceCollection.AddTransient(_ => sw);
            serviceCollection.AddTransient<TImplementation>();
            return serviceCollection;
        }
        public static IServiceCollection AddTransientByName<TService>(
            this IServiceCollection serviceCollection,
            Func<IServiceProvider, TService> implementationFactory,
            string key)
        {
            var sw = new ServiceWrap<TService>
            {
                Key = key,
                Type = implementationFactory.Invoke
            };
            serviceCollection.AddTransient(x => sw);
            return serviceCollection;
        }
        public static IServiceCollection AddSingletonByName<TService, TImplementation>(
            this IServiceCollection serviceCollection,
            string key)
            where TService : class where TImplementation : class
        {
            var sw = new ServiceWrap<TService>
            {
                Key = key,
                Type = sp => sp.GetService<TImplementation>() as TService
            };
            serviceCollection.AddSingleton(_ => sw);
            serviceCollection.AddSingleton<TImplementation>();
            return serviceCollection;
        }
        public static IServiceCollection AddSingletonByName<TService>(
            this IServiceCollection serviceCollection,
            Func<IServiceProvider, TService> implementationFactory,
            string key)
        {
            var sw = new ServiceWrap<TService>
            {
                Key = key,
                Type = implementationFactory.Invoke
            };
            serviceCollection.AddSingleton(x => sw);
            return serviceCollection;
        }
        public static T GetServiceByName<T>(this IServiceProvider serviceProvider, string key)
        {
            var services = serviceProvider.GetServices<ServiceWrap<T>>();
            var result = services.FirstOrDefault(x => x.Key == key);
            if (result == null)
            {
                return default;
            }
            return result.Type.Invoke(serviceProvider);
        }
    }
}
