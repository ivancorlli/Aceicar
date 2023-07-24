using CompanyContext.Core.Enumerable;

namespace CompanyContext.Core.Aggregate;

public sealed class Role
{
    public Guid Id {get; private set;}
    public string Name {get; private set;} = default!;
    public RoleStatus Status {get; private set;} = default!;
    internal Role(string name)
    {
        Id = Guid.NewGuid();
        Name = name.Trim().ToLower();
        Status = RoleStatus.Active;
    }
    
}