using CompanyContext.Core.Aggregate;

namespace CompanyContext.Core.Repository;

public interface IRoleRepository
{
    Task<Role?> FindById(Guid RoleId);
    Task<bool> IsNameUsed(string Name);
    Task<Role?> FindByName(string Name);
    void Update(Role Root);
}