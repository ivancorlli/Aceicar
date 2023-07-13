using CompanyContext.Core.Interface;

namespace CompanyContext.Core.ValueObject;

public sealed record CompanyArea : IArea<CompanyArea>
{
    public Guid TypeId {get;private set;} = default!;

    public Guid? SpecializationId {get;private set;} = default!;

    public static CompanyArea InSpecialization(Guid typeId, Guid specializationId)
    {
        return new CompanyArea() { TypeId = typeId, SpecializationId =specializationId};
    }

    public static CompanyArea InType(Guid typeId)
    {
        return new CompanyArea() { TypeId = typeId};
    }
}