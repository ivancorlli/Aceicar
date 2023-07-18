namespace CompanyContext.Core.Repository;

public interface IServiceRepository
{
    Task<Aggregate.Service?> GetById(Guid ServiceId);
    Task<bool> IsNameUsed(string Name);
    void Update(Aggregate.Service Root);
    // void Create(Requirement Entity);
}