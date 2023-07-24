namespace CompanyContext.Application.Feature.Type.Dto;

public sealed record TypeSummary
{
    public Guid TypeId {get;set;}
    public string Name {get;set;}  = default!;
    public string? Icon {get;set;} = default!;
}

public static class MapTypeSummary
{
    public static TypeSummary Map(
        CompanyContext.Core.Aggregate.Type type
    )
    {
        return new TypeSummary()
        {
            TypeId = type.Id,
            Name = type.Name,
            Icon = type.TypeIcon
        };
    }
}