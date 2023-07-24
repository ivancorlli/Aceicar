using CompanyContext.Core.Repository;
using CompanyContext.Infrastructure.Data;
using Marten;
using Microsoft.Extensions.Configuration;
using Wolverine;

namespace CompanyContext.Infrastructure.Repository;

public class UoW : IUoW
{

    private IDocumentSession session;
    public IBrandRepository BrandRepository { get; private set; }
    public ICompanyRepository CompanyRepository { get; private set; }
    public IProductRepository ProductRepository { get; private set; }

    public UoW(
        IConfiguration configuration,
        ICompanyStore store,
        IMessageBus bus
    )
    {
        session = store.LightweightSession();
        BrandRepository = new BrandRepository(session, store, bus);
        CompanyRepository = new CompanyRepository(session, store, bus);
        ProductRepository = new ProductRepository(session, store, bus);
    }
    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await session.SaveChangesAsync(cancellationToken);
    }
}