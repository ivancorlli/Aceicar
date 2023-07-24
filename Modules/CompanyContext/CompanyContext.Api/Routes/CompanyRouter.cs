using CompanyContext.Api.Controller.Company;

namespace CompanyContext.Api.Routes;

public static class CompanyRouter
{
    internal static IEndpointRouteBuilder CompanyEndpoints(this IEndpointRouteBuilder route)
    {
        route.MapGroup("/companies")
        .CompanyPost()
        .CompanyGet();
        return route;
    }

    private static IEndpointRouteBuilder CompanyPost(this IEndpointRouteBuilder endpoint)
    {
        endpoint.MapPatch("/{companyId}/contact-data",ChangeContactData.Execute);
        endpoint.MapPatch("/{companyId}/location",ChangeLocation.Execute);
        endpoint.MapPost("/", CreateCompany.Execute);
        return endpoint;
    }
    private static IEndpointRouteBuilder CompanyGet(this IEndpointRouteBuilder endpoint)
    {
        endpoint.MapGet("/accesses/{accessId}",CompanyLogged.Execute);
        return endpoint;
    }

}