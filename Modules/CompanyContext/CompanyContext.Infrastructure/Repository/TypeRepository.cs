using CompanyContext.Core.Aggregate;
using CompanyContext.Core.Entity;
using CompanyContext.Core.Repository;
using CompanyContext.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace CompanyContext.Infrastructure.Repository;

public sealed class TypeRepository : ITypeRepository
{
    public CompanyDbContext Context;
    public TypeRepository(CompanyDbContext context)
    {
        Context = context;
    }
    public async Task<Core.Aggregate.Type?> GetById(Guid TypeId)
    {
        Core.Aggregate.Type? data = await Context.Type.FindAsync(TypeId);
        return data;
    }

    public async Task<bool> IsNameUsed(string Name)
    {
        IList<Core.Aggregate.Type> data = await Context.Type.Where(x=>x.Name.Trim()==Name.Trim().ToLower()).ToListAsync();
        if(data.Count > 0) return true;
        return false;
    }

    public async void Update(Core.Aggregate.Type Root)
    {
         await Context.Type.AddAsync(Root);
    }

    public async void Update(Specialization Entity)
    {
        await Context.Specialization.AddAsync(Entity);
    }
}