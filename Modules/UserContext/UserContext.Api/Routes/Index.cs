using UserContext.Api.Controller;

namespace UserContext.Api.Routes;

public static class Index
{

    public static IEndpointRouteBuilder UserContextEndpoints(this IEndpointRouteBuilder route)
    {
        route.MapGroup("/user")
        .MapPosts()
        .MapGets();
        return route;
    }

    internal static IEndpointRouteBuilder MapPosts(this IEndpointRouteBuilder endpoint)
    {
        endpoint.MapPost("/new", CreateUser.Execute);
        return endpoint;
    }
    internal static IEndpointRouteBuilder MapGets(this IEndpointRouteBuilder endpoint)
    {
        endpoint.MapGet("/all",Allusers.Execute);
        return endpoint;
    }
}