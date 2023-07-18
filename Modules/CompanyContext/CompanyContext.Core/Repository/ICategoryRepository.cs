using CompanyContext.Core.Aggregate;

namespace CompanyContext.Core.Repository;

using CompanyContext.Core.Entity;

public interface ICategoryRepository 
{
    Task<Category?> GetById(Guid CategoryId);
    Task<bool> IsNameUsed(string Name);
    void Update(Category Root);
    void Update(SubCategory Entity);    
}