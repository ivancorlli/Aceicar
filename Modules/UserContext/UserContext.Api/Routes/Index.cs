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
        endpoint.MapPost("/", CreateUser.Execute);
        endpoint.MapPost("/provider",CreateUserProvider.Execute);
        endpoint.MapPatch("/{userId}/location",ModifyLocation.Execute);
        endpoint.MapPatch("/{userId}/profile",ModifyProfile.Execute);
        endpoint.MapPatch("/{userId}/email",ChangeEmail.Execute);
        endpoint.MapPatch("/{userId}/phone",ChangePhone.Execute);
        endpoint.MapPatch("/{userId}/username",ChangeUsername.Execute);
        endpoint.MapPatch("/{userId}/account",ConfigAccount.Execute);
        endpoint.MapPatch("/{userId}/timezone",ModifyTimeZone.Execute);
        return endpoint;
    }
    internal static IEndpointRouteBuilder MapGets(this IEndpointRouteBuilder endpoint)
    {
        endpoint.MapGet("/me",MyData.Execute);
        return endpoint;
    }
}