using CompanyContext.Core.Interface;

namespace CompanyContext.Core.ValueObject;

public sealed record CompanyArea : IArea
{
    public CompanyArea(Guid typeId) : base(typeId)
    {
    }

    public CompanyArea(Guid typeId, Guid specializationId) : base(typeId, specializationId)
    {
    }
}