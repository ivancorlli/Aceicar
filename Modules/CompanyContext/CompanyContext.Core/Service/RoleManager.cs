using Common.Basis.Utils;
using CompanyContext.Core.Aggregate;
using CompanyContext.Core.Error;
using CompanyContext.Core.Repository;

namespace CompanyContext.Core.Service;

public class RoleManager
{
    private readonly IRoleRepository _role;
    public RoleManager(
        IRoleRepository role
    )
    {
        _role =role;
    }

    public async Task<Result<Role>> Create( string name)
    {
        bool isUsed = await _role.IsNameUsed(name);
        if(isUsed) return Result.Fail<Role>(new RoleExists());
        Role role = new Role(name);
        return Result.Ok(role);
    }
}