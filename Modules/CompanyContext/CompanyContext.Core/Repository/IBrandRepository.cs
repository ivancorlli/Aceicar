using Common.Basis.Repository;
using CompanyContext.Core.Aggregate;
using CompanyContext.Core.Event;

namespace CompanyContext.Core.Repository;

public interface IBrandRepository : IRepository<Brand>
{
    Task<bool> IsNameUsed(string Name);
    void Create(Guid RootId, BrandCreated @event);
    void Create(Guid RootId, BrandForCompanyCreated @event);
}