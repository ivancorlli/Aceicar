using Common.Basis.Aggregate;

namespace Common.Basis.Repository;

public interface IRepository<Aggregate> where Aggregate: IAggregate
{
    void Save(Aggregate Root);
    Aggregate? FindById(Guid Id);
}