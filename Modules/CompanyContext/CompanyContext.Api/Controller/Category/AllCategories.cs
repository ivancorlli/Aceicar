using CompanyContext.Application.Feature.Category.Query.AllCategories;
using Dto = CompanyContext.Core.Aggregate.Category;
using Wolverine;

namespace CompanyContext.Api.Controller.Category;

public class AllCategories
{
    public static async Task<IResult> Execute(
        IMessageBus Bus,
        CancellationToken cancellationToken
    )
    {
        IList<Dto> data = await Bus.InvokeAsync<IList<Dto>>(new AllCategoriesCommand());
        return Results.Ok(data);
    } 
}