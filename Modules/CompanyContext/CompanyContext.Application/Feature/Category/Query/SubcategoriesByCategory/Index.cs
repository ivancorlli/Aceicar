using CompanyContext.Application.Interface;
using Microsoft.EntityFrameworkCore;
using Aggregate = CompanyContext.Core.Aggregate.Category;
using Entity = CompanyContext.Core.Entity.SubCategory;
namespace CompanyContext.Application.Feature.Category.Query.SubcategoriesByCategory;

public sealed record SubcategoriesByCategoryQuery(Guid CategoryId);
public static class SubcategoriesByCategoryHandler
{
    public static async Task<IList<Entity>> Handle(
        SubcategoriesByCategoryQuery query,
        IApplicationQuery session,
        CancellationToken cancellationToken
    )
    {
        return await session.Query<Aggregate>()
            .Include(x=>x.SubCategories)
            .Where(x=>x.Id == query.CategoryId)
            .SelectMany(x=>x.SubCategories)
            .ToListAsync(cancellationToken);
    }
    
}