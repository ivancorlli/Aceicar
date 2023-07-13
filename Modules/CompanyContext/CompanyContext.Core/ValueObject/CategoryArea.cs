using CompanyContext.Core.Interface;

namespace CompanyContext.Core.ValueObject;

public sealed record CategoryArea : IArea<CategoryArea>
{
    public Guid TypeId {get;private set;} = default!;
    /// <summary>
    /// If Specialization is null means that is related for all specifications type's, otherwise it is related only to a specific specialization 
    /// </summary>
    /// <value></value>
    public Guid? SpecializationId {get;private set;} = default!;

    private CategoryArea(){}
    public static CategoryArea InSpecialization(Guid typeId, Guid specializationId)
    {
        return new CategoryArea() {TypeId =typeId, SpecializationId = specializationId};
    }

    public static CategoryArea InType(Guid typeId)
    {
        return new CategoryArea() {TypeId = typeId};
    }
}