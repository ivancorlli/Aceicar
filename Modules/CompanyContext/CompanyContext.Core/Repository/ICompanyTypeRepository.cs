using CompanyContext.Core.Aggregate;

namespace CompanyContext.Core.Repository;

public interface ICompanyTypeRepository
{
    Task<bool> IsNameUsed(string Name);
    Task<CompanyType> GetById(Guid TypeId);
    void Create(CompanyType Root);
}