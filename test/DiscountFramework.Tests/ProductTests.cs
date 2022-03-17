using System.Threading.Tasks;
using DiscountFramework.Tests.Configuration;
using Raven.Client.Documents;
using Shouldly;

namespace DiscountFramework.Tests
{
    public class ProductTests
    {
        public async Task can_save_product()
        {
            // var product = new Product
            // {
            //     SKU = "ABC123"
            // };
            //
            // using var store = db.Store();
            // using (var session = store.OpenAsyncSession())
            // {
            //     await session.StoreAsync(product);
            //     await session.SaveChangesAsync();
            // }
            //
            // db.Wait(store);
            //
            // using (var session = store.OpenAsyncSession())
            // {
            //     var result = await session
            //                      .Query<Product>()
            //                      .Where(x => x.SKU == product.SKU, true)
            //                      .ToListAsync();
            //
            //     result.ShouldNotBeNull();
            //     result.Count.ShouldBe(1);
            // }
        }
    }
}