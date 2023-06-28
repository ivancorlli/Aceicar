namespace UserContext.Api.Routes;

public static class Index
{

    public static IEndpointRouteBuilder UserContextEndpoints(this IEndpointRouteBuilder route)
    {
        route.MapGroup("/user").Endpoints();
        return route;
    }

    internal static IEndpointRouteBuilder Endpoints(this IEndpointRouteBuilder endpoint)
    {
        endpoint.MapPost("/new",NewUser);
        return endpoint;
    }

    internal static IResult NewUser()
    {
        return Results.Ok("Enpoints Works");
    }

}