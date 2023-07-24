using Common.Basis.Repository;
using CompanyContext.Core.Aggregate;
using CompanyContext.Core.Event;

namespace CompanyContext.Core.Repository;

public interface ICompanyRepository:IRepository<Company>
{
    Task<bool> IsNameUsed(string Name);
    void Create(Guid RootId,CompanyCreated @event);

}