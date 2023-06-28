using UserContext.Api.Routes;

namespace Aceicar.WebApi.Router;

public static class Index
{
    public static IEndpointRouteBuilder ApiV1(this IEndpointRouteBuilder router)
    {
        router.MapGroup("/api")
        .UserContextEndpoints();
        return router;
    }
    
}