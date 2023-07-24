using CompanyContext.Application.Feature.Category.Query.AllSubcategories;
using CompanyContext.Core.Entity;
using Wolverine;

namespace CompanyContext.Api.Controller.Category;

public static class AllSubCategories
{
    public static async Task<IResult> Execute(
        IMessageBus Bus,
        CancellationToken cancellationToken
    )
    {
        var data = await Bus.InvokeAsync<IList<SubCategory>>(new AllSubCategoriesQuery(),cancellationToken);
        return Results.Ok(data);
    }
    
}