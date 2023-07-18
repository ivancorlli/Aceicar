using CompanyContext.Core.Entity;

namespace CompanyContext.Core.Repository;

public interface ITypeRepository
{
    Task<bool> IsNameUsed(string Name);
    Task<Aggregate.Type?> GetById(Guid TypeId);
    void Update(Aggregate.Type Root);
    void Update(Specialization Entity);
}