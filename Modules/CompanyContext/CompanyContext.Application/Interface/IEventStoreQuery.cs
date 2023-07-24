using Marten;

namespace CompanyContext.Application.Interface;

public interface IEventStoreQuery
{
     public IQuerySession Query {get;}
}