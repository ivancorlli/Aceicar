using CompanyContext.Application.ViewModel;
using CompanyContext.Core.Aggregate;
using CompanyContext.Core.Event;
using CompanyContext.Core.Repository;
using CompanyContext.Infrastructure.Data;
using Marten;
using Wolverine;

namespace CompanyContext.Infrastructure.Repository;

public sealed class ProductRepository : IProductRepository
{
    private readonly IDocumentSession _session;
    private readonly ICompanyStore _store;
    private readonly IMessageBus _bus;
    public ProductRepository(
        IDocumentSession session,
        ICompanyStore store,
        IMessageBus bus
    )
    {
        _session = session;
        _store = store;
        _bus = bus;
    }
    public void Create(Guid ProductId, ProductCreatedForCategory @event)
    {
        _session.Events.StartStream<Product>(ProductId, @event);
    }

    public void Create(Guid ProductId, ProductCreatedForSubcategory @event)
    {
        _session.Events.StartStream<Product>(ProductId, @event);
    }

    public async Task<Product?> FindById(Guid ProductId)
    {
        return await _session.Events.AggregateStreamAsync<Product>(ProductId);
    }

    public async Task<bool> IsCodeUsed(string Code)
    {
        using IQuerySession _query = _store.QuerySession();
        IReadOnlyList<ProductProjection> exits = await _query.Query<ProductProjection>().Where(x => x.Code.ToUpper().Trim() == Code.ToUpper().Trim()).ToListAsync();
        if (exits.Count > 0) return true;
        return false;
    }
}