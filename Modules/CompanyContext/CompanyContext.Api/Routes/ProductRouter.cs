using CompanyContext.Api.Controller.Product;

namespace CompanyContext.Api.Routes;

public static class ProductRouter
{
     internal static IEndpointRouteBuilder ProductEndpoints(this IEndpointRouteBuilder route)
    {
        route.MapGroup("/products")
        .Post()
        .Get();
        return route;
    }

    private static IEndpointRouteBuilder Post(this IEndpointRouteBuilder endpoint)
    {
        endpoint.MapPost("/", CreateProduct.Execute);
        return endpoint;
    }
    private static IEndpointRouteBuilder Get(this IEndpointRouteBuilder endpoint)
    {
        endpoint.MapGet("/companies/{companyId}",ProductByTypeSpecialization.Execute);
        return endpoint;
    }
}