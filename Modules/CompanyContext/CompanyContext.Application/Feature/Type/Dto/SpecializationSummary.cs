using CompanyContext.Core.Entity;

namespace CompanyContext.Application.Feature.Type.Dto;

public sealed record SpecializationSummary
{
    public Guid SpecializationId {get;set;}
    public string Name {get;set;} = default!;
    public string? Icon {get;set;} = default!;
}

public static class MapSpecializationSummary
{
    public static SpecializationSummary Map(Specialization specialization)
    {
        return new SpecializationSummary()
        {
            SpecializationId = specialization.Id,
            Name = specialization.Name,
            Icon = specialization.SpecializationIcon
        };
    }
}