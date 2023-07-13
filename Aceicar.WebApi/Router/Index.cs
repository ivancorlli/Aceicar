using UserContext.Api.Routes;


public static class Index
{
    public static IEndpointRouteBuilder ApiV1(this IEndpointRouteBuilder router)
    {
        router.MapGroup("/api")
        .RequireAuthorization()
        .UserContextEndpoints();
        return router;
    }
    
}