using CompanyContext.Api.Controller.Category;

namespace CompanyContext.Api.Routes;

public static class CategoryRouter
{
    internal static IEndpointRouteBuilder CategoryEndpoints(this IEndpointRouteBuilder route)
    {
        route.MapGroup("/categories")
        .CategoryPost()
        .CategoryGet()
        .CategoryPatch();
        return route;
    }

    private static IEndpointRouteBuilder CategoryPost(this IEndpointRouteBuilder endpoint)
    {
        endpoint.MapPost("/", NewCategory.Execute);
        endpoint.MapPost("/{categoryId}/subcategory", NewSubCategory.Execute);
        return endpoint;
    }
    private static IEndpointRouteBuilder CategoryGet(this IEndpointRouteBuilder endpoint)
    {
        endpoint.MapGet("/",AllCategories.Execute);
        endpoint.MapGet("/subcategories",AllSubCategories.Execute);
        endpoint.MapGet("/{categoryId}/subcategories",SubcategoriesByCategory.Execute);
        return endpoint;
    }

    private static IEndpointRouteBuilder CategoryPatch(this IEndpointRouteBuilder endpoint)
    {
        endpoint.MapPatch("/{categoryId}/type",AddToType.Execute);
        endpoint.MapPatch("/{categoryId}/specialization",AddToSpecialization.Execute);
        return endpoint;
    }
}