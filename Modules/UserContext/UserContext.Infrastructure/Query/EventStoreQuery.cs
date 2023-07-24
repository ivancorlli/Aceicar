using Marten;
using UserContext.Application.Interface;
using UserContext.Infrastructure.Data;

namespace UserContext.Infrastructure.Query;

public class EventStoreQuery : IApplicationQuery
{
    public IQuerySession Query {get; private set;}
    public EventStoreQuery(
        IUserStore store
    )
    {
        Query = store.QuerySession();
    }
}