using CompanyContext.Api.Controller.Type;

namespace CompanyContext.Api.Routes;

public static class TypeRoutes
{
    internal static IEndpointRouteBuilder TypeEndpoints(this IEndpointRouteBuilder route)
    {
        route.MapGroup("/types")
        .Post()
        .Get();
        return route;
    }

    private static IEndpointRouteBuilder Post(this IEndpointRouteBuilder endpoint)
    {
        endpoint.MapPost("/",CreateType.Execute);
        endpoint.MapPost("/{typeId}/specialization",CreateSpecialization.Execute);
        return endpoint;
    }
    private static IEndpointRouteBuilder Get(this IEndpointRouteBuilder endpoint)
    {
        endpoint.MapGet("/",AllTypes.Execute);
        endpoint.MapGet("/{typeId}/specialization",SpecializationsByType.Execute);
        return endpoint;
    }
}