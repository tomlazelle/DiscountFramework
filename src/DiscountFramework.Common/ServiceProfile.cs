using Microsoft.Extensions.DependencyInjection;

namespace DiscountFramework.Common
{
    public abstract class ServiceProfile
    {
        protected readonly IServiceCollection Service = new ServiceCollection();

        public IServiceCollection Register(IServiceCollection serviceCollection)
        {
            foreach (var serviceDescriptor in Service)
            {
                serviceCollection.Add(serviceDescriptor);
            }

            return serviceCollection;
        }
    }
}
