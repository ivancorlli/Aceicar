using CompanyContext.Application.Interface;
using Microsoft.EntityFrameworkCore;
using Entity = CompanyContext.Core.Entity.SubCategory;
namespace CompanyContext.Application.Feature.Category.Query.AllSubcategories;

public sealed record AllSubCategoriesQuery();
public static class AllSubcategoriesHandler
{
    public static async Task<IList<Entity>> Handle(
        AllSubCategoriesQuery query,
        IApplicationQuery session,
        CancellationToken cancellationToken
    )
    {
        return await session.Query<Entity>().Where(x=>x.Status == Core.Enumerable.CategoryStatus.Active).ToListAsync(cancellationToken);
    }
}