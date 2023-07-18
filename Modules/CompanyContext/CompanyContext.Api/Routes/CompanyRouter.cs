using CompanyContext.Api.Controller;

namespace CompanyContext.Api.Routes;

public static class CompanyRouter
{
    internal static IEndpointRouteBuilder CompanyEndpoints(this IEndpointRouteBuilder route)
    {
        route.MapGroup("/company")
        .CompanyPost()
        .CompanyGet();
        return route;
    }

    private static IEndpointRouteBuilder CompanyPost(this IEndpointRouteBuilder endpoint)
    {

        return endpoint;
    }
    private static IEndpointRouteBuilder CompanyGet(this IEndpointRouteBuilder endpoint)
    {
        return endpoint;
    }

}