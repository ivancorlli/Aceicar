using Common.Basis.Aggregate;

namespace Common.Basis.Repository;

public interface IRepository<Aggregate> where Aggregate: IAggregate
{
    void Apply(Aggregate Root);
    Task<Aggregate?> FindById(Guid Id);
}