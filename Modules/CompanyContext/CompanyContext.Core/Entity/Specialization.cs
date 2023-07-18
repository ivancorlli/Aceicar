using Common.Basis.Utils;
using CompanyContext.Core.Enumerable;

namespace CompanyContext.Core.Entity;

public sealed class Specialization
{
    public Guid Id {get;private set;} = default!;
    public string Name {get;private set;} = default!;
    public string? SpecializationIcon {get; private set;} = default!;
    public TypeStatus Status {get; private set;} = default!;
    private Specialization() {}
    internal static Result<Specialization> Create(string name)
    {
        Specialization specification = new();
        specification.Id = Guid.NewGuid();
        specification.Name = name.ToLower().Trim();
        

        return Result.Ok(specification);
    
    }
    public void Activate()
    {
        Status = TypeStatus.Active;
    }

    public void Deactivate()
    {
        Status = TypeStatus.Inactive;
    }

}