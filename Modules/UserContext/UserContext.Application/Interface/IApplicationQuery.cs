using Marten;
namespace UserContext.Application.Interface;
public interface IApplicationQuery
{
    public IQuerySession Query {get;} 
}