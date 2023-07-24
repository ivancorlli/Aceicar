using CompanyContext.Core.Aggregate;
using CompanyContext.Core.Repository;
using CompanyContext.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace CompanyContext.Infrastructure.Repository;

public sealed class RoleRepository : IRoleRepository
{
    private CompanyDbContext Context;
    public RoleRepository(
        CompanyDbContext context
    )
    {
        Context = context;
    }
    public async Task<Role?> FindById(Guid CategoryId)
    {
        return await Context.Role.FindAsync(CategoryId);
    }

    public async Task<Role?> FindByName(string Name)
    {
        IList<Role> roles = await Context.Role.Where(x => x.Name.ToLower() == Name.ToLower() && x.Status == Core.Enumerable.RoleStatus.Active).ToListAsync();
        if (roles.Count > 0) return roles.First();
        return null;
    }

    public async Task<bool> IsNameUsed(string Name)
    {
        IList<Role> roles = await Context.Role.Where(x => x.Name.ToLower() == Name.ToLower() && x.Status == Core.Enumerable.RoleStatus.Active).ToListAsync();
        if (roles.Count > 0) return true;
        return false;
    }

    public async void Update(Role Root)
    {
        await Context.Role.AddAsync(Root);
    }
}