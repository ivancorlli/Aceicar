using CompanyContext.Application.Interface;
using Microsoft.EntityFrameworkCore;
using  Aggregate = CompanyContext.Core.Aggregate.Category;

namespace CompanyContext.Application.Feature.Category.Query.AllCategories;

public sealed record AllCategoriesCommand();
public static class AllCategoriesHandler 
{
    public static async Task<IList<Aggregate>> Handle(
        AllCategoriesCommand query,
        IApplicationQuery session,
        CancellationToken cancellationToken
    )
    {
        return await session.Query<Aggregate>().ToListAsync(cancellationToken);
    }
    
}