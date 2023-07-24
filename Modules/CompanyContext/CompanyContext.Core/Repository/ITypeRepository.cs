using CompanyContext.Core.Entity;

namespace CompanyContext.Core.Repository;

public interface ITypeRepository
{
    Task<bool> IsNameUsed(string Name);
    Task<Aggregate.Type?> FindById(Guid TypeId);
    Task<Specialization?> FindById(Guid TypeId, Guid SpecializationId);
    void Update(Aggregate.Type Root);
    void Update(Specialization Entity);
}