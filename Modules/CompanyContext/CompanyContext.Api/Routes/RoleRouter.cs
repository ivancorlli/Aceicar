using CompanyContext.Api.Controller.Role;

namespace CompanyContext.Api.Routes;

public static class RoleRouter
{
    internal static IEndpointRouteBuilder RoleEndpoints(this IEndpointRouteBuilder route)
    {
        route.MapGroup("/roles")
        .Post()
        .Get();
        return route;
    }

    private static IEndpointRouteBuilder Post(this IEndpointRouteBuilder endpoint)
    {
        endpoint.MapPost("/",CreateRole.Execute);
        return endpoint;
    }
    private static IEndpointRouteBuilder Get(this IEndpointRouteBuilder endpoint)
    {
        return endpoint;
    }
}