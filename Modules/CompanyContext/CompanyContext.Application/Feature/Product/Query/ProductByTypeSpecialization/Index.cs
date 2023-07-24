using CompanyContext.Application.Interface;
using CompanyContext.Application.ViewModel;
using CompanyContext.Core.Entity;
using Microsoft.EntityFrameworkCore;
using Marten;

namespace CompanyContext.Application.Feature.Product.Query.ProductByTypeSpecialization;

public sealed record ProductByTypeSpecializationQuery(Guid CompanyId);
public static class ProductByTypeSpecializationHandler
{
    public static Task<List<ProductProjection>> Handle(
        ProductByTypeSpecializationQuery query,
        IApplicationQuery querySession,
        IEventStoreQuery queryStore,
        CancellationToken cancellationToken
    )
    {

        List<ProductProjection> products = queryStore.Query.Query<ProductProjection>().ToList();
        return Task.FromResult(products);
    }
}