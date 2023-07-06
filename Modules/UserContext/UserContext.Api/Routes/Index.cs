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
        endpoint.MapPost("/new-provider",CreateUserProvider.Execute);
        endpoint.MapPost("/location",ModifyLocation.Execute);
        endpoint.MapPost("/profile",ModifyProfile.Execute);
        endpoint.MapPost("/email",ChangeEmail.Execute);
        endpoint.MapPost("/phone",ChangePhone.Execute);
        endpoint.MapPost("/username",ChangeUsername.Execute);
        endpoint.MapPost("/account",ConfigAccount.Execute);
        return endpoint;
    }
    internal static IEndpointRouteBuilder MapGets(this IEndpointRouteBuilder endpoint)
    {
        endpoint.MapGet("/me",GetMe.Execute).RequireAuthorization();
        return endpoint;
    }
}