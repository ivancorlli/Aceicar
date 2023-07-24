using CompanyContext.Core.Aggregate;
using CompanyContext.Core.Repository;
using CompanyContext.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace CompanyContext.Infrastructure.Repository;

public sealed class ServiceRepository : IServiceRepository
{
    private readonly CompanyDbContext Context;
    public ServiceRepository(
        CompanyDbContext context
    )
    {
        Context = context;
    }
    public async void Update(Service Root)
    {
        await Context.Service.AddAsync(Root);
    }

    public async Task<Service?> FindById(Guid ServiceId)
    {
        return await Context.Service.FindAsync(ServiceId);
    }
    public async Task<bool> IsNameUsed(string Name)
    {
        IList<Service> services = await Context.Service.Where(x => x.Name == Name.Trim().ToLower()).ToListAsync();
        if (services.Count > 0) return true;
        else return false;
    }
}