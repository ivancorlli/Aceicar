using CompanyContext.Application.Feature.Product.Query.ProductByTypeSpecialization;
using CompanyContext.Application.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Wolverine;

namespace CompanyContext.Api.Controller.Product;

public static class ProductByTypeSpecialization
{
    public static async Task<IResult> Execute(
        [FromRoute] Guid companyId,
        IMessageBus bus,
        CancellationToken cancellationToken
    )
    {
        IList<ProductProjection> products = await bus.InvokeAsync<List<ProductProjection>>(new ProductByTypeSpecializationQuery(companyId), cancellationToken);
        return TypedResults.Ok(products);
    }
}