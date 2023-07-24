using CompanyContext.Application.Interface;
using CompanyContext.Infrastructure.Context;

namespace CompanyContext.Infrastructure.Query;

public class EfCoreQuery:IApplicationQuery
{
    private readonly CompanyDbContext Context;
    public EfCoreQuery(CompanyDbContext context)
    {
        Context = context;
    }
    
    public IQueryable<T> Query<T>() where T:class
    {
        return Context.Set<T>();
    }
}