namespace CompanyContext.Core.ValueObject;

public sealed record CompanyArea
{
    public Guid TypeId { get; private set; }
    public Guid SpecializationId { get; private set; }

    internal CompanyArea(Guid typeId, Guid specializationId)
    {
        TypeId = typeId;
        SpecializationId = specializationId;
    }
}