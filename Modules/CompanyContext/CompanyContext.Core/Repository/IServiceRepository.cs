namespace CompanyContext.Core.Repository;

public interface IServiceRepository
{
    Task<Aggregate.Service?> FindById(Guid ServiceId);
    Task<bool> IsNameUsed(string Name);
    void Update(Aggregate.Service Root);
}