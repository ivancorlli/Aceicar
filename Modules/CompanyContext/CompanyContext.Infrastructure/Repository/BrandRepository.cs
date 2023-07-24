using CompanyContext.Core.Aggregate;
using CompanyContext.Core.Event;
using CompanyContext.Core.Repository;
using CompanyContext.Infrastructure.Data;
using Marten;
using Wolverine;

namespace CompanyContext.Infrastructure.Repository;

public class BrandRepository : IBrandRepository
{
    private readonly IDocumentSession _session;
    private readonly ICompanyStore _store;
    private readonly IMessageBus _bus;
    public BrandRepository(
        IDocumentSession session,
        ICompanyStore store,
        IMessageBus bus
    )
    {
        _session = session;
        _store = store;
        _bus = bus;
    }

    public void Apply(Brand Root)
    {
        throw new NotImplementedException();
    }

    public void Create(Guid RootId, BrandCreated @event)
    {
        throw new NotImplementedException();
    }

    public void Create(Guid RootId, BrandForCompanyCreated @event)
    {
        throw new NotImplementedException();
    }

    public Task<Brand?> FindById(Guid Id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> IsNameUsed(string Name)
    {
        throw new NotImplementedException();
    }

    public Task Push<T>(T IntegrationEvent)
    {
        throw new NotImplementedException();
    }
}