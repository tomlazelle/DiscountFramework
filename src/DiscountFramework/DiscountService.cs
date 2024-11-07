using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using DiscountFramework.Containers;
using DiscountFramework.Services;
using Raven.Client.Documents;

namespace DiscountFramework;

public class DiscountService : IDiscountService
{
    private readonly IDiscountVisitor _discountVisitor;
    private readonly IDocumentStore _documentStore;
    private readonly IMapper _mapper;

    public DiscountService(
        IDocumentStore documentStore,
        IDiscountVisitor discountVisitor,
        IMapper mapper)
    {
        _documentStore = documentStore;
        _discountVisitor = discountVisitor;
        _mapper = mapper;
    }

    public async Task<DiscountResult> ApplyDiscount(Cart cart, string couponCode)
    {
        var predicate = new PredicateBuilder<Discount>()
            .Initial(x => x.TenantId == cart.TenantId && x.Enabled);

        if (!string.IsNullOrEmpty(couponCode))
        {
            // add the discount code to the where clause
            predicate.And(x => x.CouponCode == couponCode);
        }

        using var session = _documentStore.OpenAsyncSession();
        var productDiscounts = await session.Query<Discount>()
            .Customize(x => x.WaitForNonStaleResults(TimeSpan.FromSeconds(5)))
            .Where(predicate.ToExpressionPredicate())
            .ToListAsync();

        var discountCart = _mapper.Map<DiscountCart>(cart);

        if (productDiscounts == null || productDiscounts.Count == 0)
        {
            return new DiscountResult
            {
                Cart = discountCart,
                Success = false
            };
        }


        await _discountVisitor.Visit(discountCart, productDiscounts);


        return new DiscountResult
        {
            Cart = discountCart,
            Success = true
        };
    }
}