namespace CompanyContext.Api.Routes;

public static class Index
{
    public static IEndpointRouteBuilder CompanyContextEndpoints(this IEndpointRouteBuilder route)
    {
        route.CompanyEndpoints();
        route.CategoryEndpoints();
        return route;
    }


}