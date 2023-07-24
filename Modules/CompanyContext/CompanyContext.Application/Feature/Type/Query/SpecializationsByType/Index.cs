using CompanyContext.Application.Feature.Type.Dto;
using CompanyContext.Application.Interface;
using CompanyContext.Core.Entity;
using Microsoft.EntityFrameworkCore;

namespace CompanyContext.Application.Feature.Type.Query.SpecializationsForType;

public sealed record SpecializationByTypeQuery(Guid TypeId);
public static class SpecializationsForTypeHandler
{
    public static async Task<IList<SpecializationSummary>> Handle(
        SpecializationByTypeQuery query,
        IApplicationQuery session,
        CancellationToken cancellationToken
    )
    {
        IList<Specialization> list = await session.Query<CompanyContext.Core.Aggregate.Type>()
            .Include(x => x.Specializations)
            .Where(x => x.Id == query.TypeId && x.Status == Core.Enumerable.TypeStatus.Active)
            .SelectMany(x => x.Specializations)
            .ToListAsync(cancellationToken);
        IList<SpecializationSummary> summary = new List<SpecializationSummary>();
        foreach (Specialization item in list)
        {
            SpecializationSummary newSummary = MapSpecializationSummary.Map(item);
            summary.Add(newSummary);   
        }
        return summary;
    }
}