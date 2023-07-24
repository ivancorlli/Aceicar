namespace CompanyContext.Api.Routes;

public static class Index
{
    public static IEndpointRouteBuilder CompanyContextEndpoints(this IEndpointRouteBuilder route)
    {
        route.CompanyEndpoints();
        route.CategoryEndpoints();
        route.TypeEndpoints();
        route.RoleEndpoints();
        route.AccessEndpoints();
        route.ProductEndpoints();
        return route;
    }


}