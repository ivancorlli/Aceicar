using Marten;
using Microsoft.Extensions.Configuration;
using UserContext.Core.Repository;
using UserContext.Infrastructure.Data;
using Wolverine;

namespace UserContext.Infrastructure.Repository;

public class UnitOfWork : IUoW
{
    private IDocumentSession session;
    public IUserRepository UserRepository { get; private set; }

    public UnitOfWork(
        IConfiguration configuration,
        IUserStore store,
        IMessageBus bus
    )
    {
        session = store.LightweightSession();
        UserRepository = new UserRepository(session,store,bus);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await session.SaveChangesAsync(cancellationToken);
    }
}