using Common.Basis.Error;

namespace CompanyContext.Core.Error;

public record RoleExists:DomainError
{
    public RoleExists() : base(ErrorTypes.TypeBuilder(nameof(CompanyContext),nameof(RoleExists)), "Role name already used"){}
}
public record RoleNotFound:DomainError
{
    public RoleNotFound() : base(ErrorTypes.TypeBuilder(nameof(CompanyContext.Core),nameof(RoleNotFound)), "Role not found"){}
}