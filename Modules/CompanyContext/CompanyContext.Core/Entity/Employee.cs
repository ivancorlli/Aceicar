using CompanyContext.Core.Enumerable;

namespace CompanyContext.Core.Entity;

public sealed class Employee
{
    public Guid Id {get; private set;} = default!;
    public Guid UserId { get; private set; } = default!;
    public EmployeeStatus Status { get; private set; } = default!;

    internal Employee(
        Guid id,
        Guid userId
    )
    {
        Id = id;
        UserId = userId;
        Status = EmployeeStatus.Active;
    }

    public void Deactivate()
    {
        Status = EmployeeStatus.Inactive;
    }
    public void Delete()
    {
        Status = EmployeeStatus.Deleted;
    }

}