using CompanyContext.Core.Interface;

namespace CompanyContext.Core.ValueObject;

public sealed record CategoryArea : IArea
{
    internal CategoryArea(Guid typeId) : base(typeId)
    {
    }

    internal CategoryArea(Guid typeId, Guid specializationId) : base(typeId, specializationId)
    {
    }
}