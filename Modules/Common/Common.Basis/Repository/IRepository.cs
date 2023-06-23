using Common.Basis.Aggregate;

namespace Common.Basis.Repository;

public interface IRepository<Aggregate,Id> where Aggregate: IAggregate<Id>
{
    void Save(Aggregate Root);
    Aggregate? FindById(Id Id);
}