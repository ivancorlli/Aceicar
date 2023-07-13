using CompanyContext.Core.Enumerable;

namespace CompanyContext.Core.Entity;

public sealed class Employee
{
    public Guid UserId {get;private set;} =default!;
    public Guid RoleId {get;private set;} = default!;
    public EmployeeStatus Status {get;private set;} = default!;
}