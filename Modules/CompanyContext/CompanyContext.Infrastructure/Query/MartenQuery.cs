using CompanyContext.Application.Interface;
using CompanyContext.Infrastructure.Data;
using Marten;

namespace CompanyContext.Infrastructure.Query;

public sealed class MartenQuery : IEventStoreQuery
{
    public IQuerySession Query {get;private set;}
    public MartenQuery(
        ICompanyStore store
    )
    {
        using IQuerySession _query = store.QuerySession();
        Query = _query;
    }
}