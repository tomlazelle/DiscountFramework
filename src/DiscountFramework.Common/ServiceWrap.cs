using System;

namespace DiscountFramework.Common
{
    internal class ServiceWrap<T>
    {
        public string Key { get; set; }
        public Func<IServiceProvider, T> Type { get; set; }
    }
}