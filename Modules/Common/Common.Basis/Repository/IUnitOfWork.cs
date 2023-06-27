namespace Common.Basis.Repository;

    public interface IUnitOfWork:IDisposable
    {
        Task SaveChangesAsync(CancellationToken cancellationToken);
        void OutboxMessage();
        void AuditableEntity();
    }