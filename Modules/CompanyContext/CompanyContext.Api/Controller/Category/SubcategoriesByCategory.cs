using CompanyContext.Application.Feature.Category.Query.SubcategoriesByCategory;
using CompanyContext.Core.Entity;
using Wolverine;

namespace CompanyContext.Api.Controller.Category;

public static class SubcategoriesByCategory
{
    public static async Task<IResult> Execute(
        Guid categoryId,
        IMessageBus Bus,
        CancellationToken cancellationToken
    )   
    {
        var data = await Bus.InvokeAsync<IList<SubCategory>>(new SubcategoriesByCategoryQuery(categoryId),cancellationToken);
        return Results.Ok(data);
    }
}