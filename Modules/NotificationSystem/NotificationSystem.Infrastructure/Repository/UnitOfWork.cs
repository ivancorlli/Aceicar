using Marten;
using Microsoft.Extensions.Configuration;
using NotificationSystem.Core.Repository;
using NotificationSystem.Infrastructure.Data;

namespace NotificationSystem.Infrastructure.Repository;

public class UnitOfWork : IUoW
{
    private IDocumentSession Session;
    public IUserRepository UserRepository { get; private set; }

    public UnitOfWork(
        IUserRepository userRepo,
        IConfiguration configuration,
        INotficiationSystem store
        )
    {
        Session = store.LightweightSession();
        UserRepository = new UserRepository(Session);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await Session.SaveChangesAsync(cancellationToken);
    }
}