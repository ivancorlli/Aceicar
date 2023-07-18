using CompanyContext.Core.Interface;

namespace CompanyContext.Core.ValueObject;

public sealed record ServiceArea : IArea
{
    public ServiceArea(Guid typeId) : base(typeId)
    {
    }

    public ServiceArea(Guid typeId, Guid specializationId) : base(typeId, specializationId)
    {
    }
}