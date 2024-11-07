using System.Reflection;
using System.Text;
using DiscountFramework;
using Shouldly;

namespace Discount.Tests.Configuration;

public static class TestHelper
{

    public static decimal GetCartTotalWithTax(this DiscountCart cart)
    {
        var totalAmount = cart.DiscountItems.Sum(x =>
        {
            // Use DiscountedAmount if available; otherwise, default to Amount
            var itemTotal = x.Amount * x.Quantity;

            if (cart.DiscountPercentage > 0)
            {
                var discountAmount = itemTotal * cart.DiscountPercentage;
                itemTotal -= discountAmount;
            }

            if (x.Discount is > 0)
            {
                itemTotal -= x.Discount.Value;
            }

            // Apply tax
            if (x.Taxable && itemTotal > 0)
            {
                return itemTotal * (1 + cart.TaxRate);
            }

            return itemTotal;
        });

        return totalAmount;
    }
    public static void PropertyShouldNotBeNull(this object dynamicObject, string propertyName)
    {
        object result = dynamicObject.GetType().GetProperty(propertyName)!;

        result.ShouldNotBeNull();
    }


    public static string GetTestData(string key)
    {
        var assembly = typeof(TestHelper).GetTypeInfo().Assembly;
        var resourceStream = assembly.GetManifestResourceStream($"PB.SLOrderParse.Tests.TestData.{key}");

        if (resourceStream == null)
        {
            throw new Exception($"Resource {key} not found in {assembly.FullName}");
        }

        using var reader = new StreamReader(resourceStream, Encoding.UTF8);
        return reader.ReadToEnd();
    }

    public static string[] ToArray(this string data)
    {
        var result = new List<string>();
        var reader = new StringReader(data);
        while (reader.Peek() > 0)
        {
            result.Add(reader.ReadLine());
        }

        return result.ToArray();
    }
}