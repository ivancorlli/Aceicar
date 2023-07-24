using Common.Basis.Error;

namespace CompanyContext.Core.Error;

public record CompanyNotFound:DomainError
{
    public CompanyNotFound() : base(ErrorTypes.TypeBuilder(nameof(CompanyContext.Core),nameof(CompanyNotFound)), "Company not found"){}
}
public record CompanyInactive:DomainError
{
    public CompanyInactive() : base(ErrorTypes.TypeBuilder(nameof(CompanyContext.Core),nameof(CompanyInactive)), "Company inactive"){}
}
public record CompanyExists:DomainError
{
    public CompanyExists() : base(ErrorTypes.TypeBuilder(nameof(CompanyContext.Core),nameof(CompanyExists)), "Company name used"){}
}
public record AreaAlreadyRegistered:DomainError
{
    public AreaAlreadyRegistered() : base(ErrorTypes.TypeBuilder(nameof(CompanyContext.Core),nameof(AreaAlreadyRegistered)), "Company already registered in area"){}
}
public record EmployeeAlreadyRegistered:DomainError
{
    public EmployeeAlreadyRegistered() : base(ErrorTypes.TypeBuilder(nameof(CompanyContext.Core),nameof(EmployeeAlreadyRegistered)), "User already registered as employee"){}
}
public record EmployeeNotFound:DomainError
{
    public EmployeeNotFound() : base(ErrorTypes.TypeBuilder(nameof(CompanyContext.Core),nameof(EmployeeNotFound)), "Employee not found in company"){}
}
public record AccessAlreadyRegistered:DomainError
{
    public AccessAlreadyRegistered() : base(ErrorTypes.TypeBuilder(nameof(CompanyContext.Core),nameof(AccessAlreadyRegistered)), "User already registered in company"){}
}
public record AccessNotFound:DomainError
{
    public AccessNotFound() : base(ErrorTypes.TypeBuilder(nameof(CompanyContext.Core),nameof(AccessNotFound)), "Access not found in company"){}
}
public record AccessIsDeleted:DomainError
{
    public AccessIsDeleted() : base(ErrorTypes.TypeBuilder(nameof(CompanyContext.Core),nameof(AccessIsDeleted)), "You do not have valid access"){}
}
public record AccessIsInactive:DomainError
{
    public AccessIsInactive() : base(ErrorTypes.TypeBuilder(nameof(CompanyContext.Core),nameof(AccessIsInactive)), "Your access is inactive"){}
}