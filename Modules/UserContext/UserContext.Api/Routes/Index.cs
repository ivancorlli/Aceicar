using UserContext.Api.Controller;

namespace UserContext.Api.Routes;

public static class Index
{

    public static IEndpointRouteBuilder UserContextEndpoints(this IEndpointRouteBuilder route)
    {
        route.MapGroup("/users")
        .MapPosts()
        .MapGets();
        return route;
    }

    internal static IEndpointRouteBuilder MapPosts(this IEndpointRouteBuilder endpoint)
    {
        endpoint.MapPost("/new", CreateUser.Execute).RequireAuthorization();
        endpoint.MapPost("/new-provider",CreateUserProvider.Execute).RequireAuthorization();
        endpoint.MapPatch("/{userId}/location",ModifyLocation.Execute).RequireAuthorization();
        endpoint.MapPatch("/{userId}/profile",ModifyProfile.Execute).RequireAuthorization();
        endpoint.MapPatch("/{userId}/email",ChangeEmail.Execute).RequireAuthorization();
        endpoint.MapPatch("/{userId}/phone",ChangePhone.Execute).RequireAuthorization();
        endpoint.MapPatch("/{userId}/username",ChangeUsername.Execute).RequireAuthorization();
        endpoint.MapPatch("/{userId}/account",ConfigAccount.Execute).RequireAuthorization();
        return endpoint;
    }
    internal static IEndpointRouteBuilder MapGets(this IEndpointRouteBuilder endpoint)
    {
        endpoint.MapGet("/me",GetMe.Execute).RequireAuthorization();
        return endpoint;
    }
}