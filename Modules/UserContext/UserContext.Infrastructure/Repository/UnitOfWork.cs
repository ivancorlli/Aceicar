using Marten;
using UserContext.Core.Repository;

namespace UserContext.Infrastructure.Repository;

public class UnitOfWork : IUoW
{
    private IDocumentSession Session;
    public IUserRepository UserRepository { get; private set; }

    public UnitOfWork(
        IUserRepository userRepo,
        IDocumentSession session
        )
    {
        Session = session;
        UserRepository = userRepo;
    }

    public void AuditableEntity()
    {
        throw new NotImplementedException();
    }

    public void Dispose()
    {
       Session.Dispose(); 
    }

    public void OutboxMessage()
    {
        throw new NotImplementedException();
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await Session.SaveChangesAsync(cancellationToken);
    }
}