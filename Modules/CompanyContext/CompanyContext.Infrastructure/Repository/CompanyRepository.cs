using CompanyContext.Application.ViewModel;
using CompanyContext.Core.Aggregate;
using CompanyContext.Core.Event;
using CompanyContext.Core.Repository;
using CompanyContext.Infrastructure.Data;
using Marten;
using Wolverine;

namespace CompanyContext.Infrastructure.Repository;

public class CompanyRepository : ICompanyRepository
{

    private readonly IDocumentSession _session;
    private readonly ICompanyStore _store;
    private readonly IMessageBus _bus;
    public CompanyRepository(
        IDocumentSession session,
        ICompanyStore store,
        IMessageBus bus
    )
    {
        _session = session;
        _store = store;
        _bus = bus;
    }
    public void Apply(Company Root)
    {
        Guid Id = Root.Id;
        IEnumerable<object> events = Root.Events;
        if (events.ToList().Count > 0)
        {
            foreach (var item in events.ToList())
            {
                _session.Events.Append(Id, item);
            }

        }
        Root.Clear();
    }

    public void Create(Guid RootId, CompanyCreated @event)
    {
        _session.Events.StartStream<Company>(RootId, @event);
    }

    public async Task<Company?> FindById(Guid Id)
    {
        using IQuerySession _query = _store.QuerySession();
        var result = await _query.Events.AggregateStreamAsync<Company>(Id);
        if (result != null) return result;
        else return null;
    }

    public async Task<bool> IsNameUsed(string Name)
    {
        using IQuerySession _query = _store.QuerySession();
        IEnumerable<CompanyProjection> result = await _query.Query<CompanyProjection>().Where(x=>x.Name == Name.Trim().ToLower()).ToListAsync();
        if (result.Count() > 0) return true;
        else return false;
    }

    public Task Push<T>(T IntegrationEvent)
    {
        throw new NotImplementedException();
    }
}