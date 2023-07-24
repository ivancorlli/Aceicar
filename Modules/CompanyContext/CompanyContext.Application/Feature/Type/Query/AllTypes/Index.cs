using CompanyContext.Application.Feature.Type.Dto;
using CompanyContext.Application.Interface;
using Microsoft.EntityFrameworkCore;

namespace CompanyContext.Application.Feature.Type.Query.AllTypes;

public sealed record AllTypesQuery();
public  static class AllTypesHandler
{
    public static async Task<IList<TypeSummary>> Handle(
        AllTypesQuery query,
        IApplicationQuery session,
        CancellationToken cancellationToken
    )
    {
        return await session.Query<CompanyContext.Core.Aggregate.Type>()
            .Where(x=>x.Status == Core.Enumerable.TypeStatus.Active)
            .Select(x=> MapTypeSummary.Map(x))
            .ToListAsync();
    }   
}