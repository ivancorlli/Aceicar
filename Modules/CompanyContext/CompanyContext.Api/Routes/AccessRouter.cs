using CompanyContext.Api.Controller.Access;

namespace CompanyContext.Api.Routes;

public static class AccessRouter
{
    internal static IEndpointRouteBuilder AccessEndpoints(this IEndpointRouteBuilder route)
    {
        route.MapGroup("/accesses")
        .Post()
        .Get();
        return route;
    }

    private static IEndpointRouteBuilder Post(this IEndpointRouteBuilder endpoint)
    {
        return endpoint;
    }
    private static IEndpointRouteBuilder Get(this IEndpointRouteBuilder endpoint)
    {
        endpoint.MapGet("/users/{userId}", UserAccesses.Execute);
        endpoint.MapGet("/{accessId}",AccessById.Execute);
        return endpoint;
    }
}