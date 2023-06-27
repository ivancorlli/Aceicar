using Common.Basis.Aggregate;

namespace Common.Basis.Repository;

public interface IRepository<Aggregate> where Aggregate: IAggregate
{
    void Apply(Guid Id, params object[] @events);
    Task<Aggregate?> FindById(Guid Id);
}